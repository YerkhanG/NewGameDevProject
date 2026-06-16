using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using encounter_system.data;
using encounter_system.managers;
using map_encounter_system.map_system.data;
using map_encounter_system.map_system.data.node;
using UnityEngine;
using Random = UnityEngine.Random;

namespace map_encounter_system.map_system
{
    public class MapGenerator : MonoBehaviour
    {
        //here everything has to e constructed in mapConfig. Be it the layers , number of nodes etc. 
        public MapConfig config;
        public List<float> layerDistances;
        //List for all layers with nodes 
        public List<List<Node>> nodes = new List<List<Node>>();
        
        
        public MapGenerator(MapConfig config)
        {
            this.config = config;
        }

        public Map GenerateMap()
        {
            if (nodes == null)
            {
                nodes = new List<List<Node>>();
            }
            if (config == null)
            {
                Debug.LogError("config is null");
                return null;
            }

            GenerateLayerDistances();
            //Place layers first
            for (int i = 0; i < config.layerCount; i++)
            {
                PlaceLayer(i);
            }
            
            List<List<Vector2Int>> paths = GeneratePaths();
            Debug.Log("paths: " + paths.Count);
            RandomizeNodePositions();
            SetUpConnections(paths);
            RemoveCrossConnections();
            RemoveNotUsedNodes();
            //Then i have to path em 
            //Return Map
            return new Map(config.name,"Some random shit", nodes, new List<Vector2Int>());
        }

        private void RemoveNotUsedNodes()
        {
            foreach (List<Node> list in nodes)
            {
                foreach (Node node in list.ToList())
                {
                    if (node.connections2.Count == 0 && node.isConnectedTo == false)
                    {
                        list.Remove(node);
                    }
                }
            }
        }

        private void RandomizeNodePositions()
        {
            for (int index = 0; index < nodes.Count; index++)
            {
                List<Node> list = nodes[index];
                MapLayer layer = config.layers[index];
                float distToNextLayer = index + 1 >= layerDistances.Count
                    ? 0f
                    : layerDistances[index + 1];
                float distToPreviousLayer = layerDistances[index];

                foreach (Node node in list)
                {
                    float xRnd = Random.Range(-0.5f, 0.5f);
                    float yRnd = Random.Range(-0.5f, 0.5f);

                    float x = xRnd * layer.nodesApartDistance;
                    float y = yRnd < 0 ? distToPreviousLayer * yRnd: distToNextLayer * yRnd;

                    node.position += new Vector2(x, y) * layer.randomizeNodes;
                }
            }
        }

        private Node GetNode(Vector2Int p)
        {
            if (p.y >= nodes.Count) return null;
            if (p.x >= nodes[p.y].Count) return null;

            return nodes[p.y][p.x];
        }
        private void SetUpConnections(List<List<Vector2Int>> paths)
        {
            foreach (List<Vector2Int> path in paths)
            {
                for (int i = 0; i < path.Count - 1; ++i)
                {
                    Node node = GetNode(path[i]);
                    Node nextNode = GetNode(path[i + 1]);
                    node.AddConnection(nextNode);
                    nextNode.isConnectedTo = true;
                }
            }
        }

        //i have to spawn every node by every layer??
        public void PlaceLayer(int i)
        {
               MapLayer layer = config.layers[i];
               List<Node> nodesOnThisLayer =  new List<Node>();

               float offset = layer.nodesApartDistance * (config.widthNodeCount - 1 )/ 2f ;

               for (int j = 0; j < config.widthNodeCount; j++)
               {
                   // 1. Determine the Rarity/Type based on layer rules
                   Encounter.Rarity nodeType = (Random.value < layer.randomizeNodes)
                       ? GetRandomType()
                       :layer.defaultNodeType;
                       
                   
                   Encounter encounter = EncounterManager.instance.PickRandomEncounterByRarity(nodeType);

                   // 3. Create the Node as a Data Container
                   Node node = new Node(nodeType, encounter, new Vector2Int(j, i))
                   {
                       position = new Vector2(-offset + j * layer.nodesApartDistance,-3 + GetDistanceToLayer(i))
                   };
                    
                   nodesOnThisLayer.Add(node);
                   Debug.Log("node position: " + node.position);
               }
               Debug.Log(nodesOnThisLayer.Count);
               Debug.Log(nodes);
               nodes.Add(nodesOnThisLayer);
        }

        private Vector2Int GetFinalNode()
        {
            int y = config.layers.Count - 1;
            if (config.widthNodeCount % 2 == 1)
                return new Vector2Int(config.widthNodeCount / 2, y);

            return Random.Range(0, 2) == 0
                ? new Vector2Int(config.widthNodeCount / 2, y)
                : new Vector2Int(config.widthNodeCount / 2 - 1, y);
        }
        private List<List<Vector2Int>> GeneratePaths()
        {
            Vector2Int finalNode = GetFinalNode();
            var paths = new List<List<Vector2Int>>();
            int numOfStartingNodes = config.numOfStartingNodes.GetValue();
            int numOfPreBossNodes = config.numOfPreBossNodes.GetValue();

            List<int> candidateXs = new List<int>();
            for (int i = 0; i < config.widthNodeCount; i++)
                candidateXs.Add(i);

            candidateXs = candidateXs.OrderBy(x => Guid.NewGuid()).ToList();
            IEnumerable<int> startingXs = candidateXs.Take(numOfStartingNodes);
            List<Vector2Int> startingPoints = (from x in startingXs select new Vector2Int(x, 0)).ToList();

            candidateXs = candidateXs.OrderBy(x => Guid.NewGuid()).ToList();
            IEnumerable<int> preBossXs = candidateXs.Take(numOfPreBossNodes);
            List<Vector2Int> preBossPoints = (from x in preBossXs select new Vector2Int(x, finalNode.y - 1)).ToList();

            int numOfPaths = Mathf.Max(numOfStartingNodes, numOfPreBossNodes) + Mathf.Max(0, config.extraPaths);
            for (int i = 0; i < numOfPaths; ++i)
            {
                Vector2Int startNode = startingPoints[i % numOfStartingNodes];
                Vector2Int endNode = preBossPoints[i % numOfPreBossNodes];
                List<Vector2Int> path = Path(startNode, endNode);
                path.Add(finalNode);
                paths.Add(path);
            }

            return paths;
        }
        private void RemoveCrossConnections()
        {
            for (int i = 0; i < config.widthNodeCount - 1; ++i)
                for (int j = 0; j < config.layers.Count - 1; ++j)
                {
                    Node node = GetNode(new Vector2Int(i, j));
                    if (node == null || node.HasNoConnections()) continue;
                    Node right = GetNode(new Vector2Int(i + 1, j));
                    if (right == null || right.HasNoConnections()) continue;
                    Node top = GetNode(new Vector2Int(i, j + 1));
                    if (top == null || top.HasNoConnections()) continue;
                    Node topRight = GetNode(new Vector2Int(i + 1, j + 1));
                    if (topRight == null || topRight.HasNoConnections()) continue;

                    // Debug.Log("Inspecting node for connections: " + node.point);
                    if (!node.connections2.Any(element => element.Equals(topRight.gridPosition))) continue;
                    if (!right.connections2.Any(element => element.Equals(top.gridPosition))) continue;

                    Debug.Log("Found a cross node: " + node);

                    // we managed to find a cross node:
                    // 1) add direct connections:
                    node.AddConnection(top);

                    right.AddConnection(topRight);

                    float rnd = Random.Range(0f, 1f);
                    if (rnd < 0.2f)
                    {
                        // remove both cross connections:
                        // a) 
                        node.connections2.Remove(topRight.gridPosition);
                        // b) 
                        right.connections2.Remove(top.gridPosition);
                    }
                    else if (rnd < 0.6f)
                    {
                        // a) 
                        node.connections2.Remove(top.gridPosition);
                    }
                    else
                    {
                        // b) 
                        right.connections2.Remove(topRight.gridPosition);
                    }
                }
        }
        // Generates a random path bottom up.
        private List<Vector2Int> Path(Vector2Int fromPoint, Vector2Int toPoint)
        {
            int toRow = toPoint.y;
            int toCol = toPoint.x;

            int lastNodeCol = fromPoint.x;

            List<Vector2Int> path = new List<Vector2Int> { fromPoint };
            List<int> candidateCols = new List<int>();
            for (int row = 1; row < toRow; ++row)
            {
                candidateCols.Clear();

                int verticalDistance = toRow - row;
                int horizontalDistance;

                int forwardCol = lastNodeCol;
                horizontalDistance = Mathf.Abs(toCol - forwardCol);
                if (horizontalDistance <= verticalDistance)
                    candidateCols.Add(lastNodeCol);

                int leftCol = lastNodeCol - 1;
                horizontalDistance = Mathf.Abs(toCol - leftCol);
                if (leftCol >= 0 && horizontalDistance <= verticalDistance)
                    candidateCols.Add(leftCol);

                int rightCol = lastNodeCol + 1;
                horizontalDistance = Mathf.Abs(toCol - rightCol);
                if (rightCol < config.widthNodeCount && horizontalDistance <= verticalDistance)
                    candidateCols.Add(rightCol);

                int randomCandidateIndex = Random.Range(0, candidateCols.Count - 1);
                int candidateCol = candidateCols[randomCandidateIndex];
                Vector2Int nextPoint = new Vector2Int(candidateCol, row);

                path.Add(nextPoint);

                lastNodeCol = candidateCol;
            }

            path.Add(toPoint);

            return path;
        }
        private float GetDistanceToLayer(int layerIndex)
        {
            if (layerIndex < 0 || layerIndex > layerDistances.Count) return 0f;

            return layerDistances.Take(layerIndex + 1).Sum();
        }

        private Encounter.Rarity GetRandomType()
        {
            int randomIndex = Random.Range(0, config.randomTypes.Count);
            return config.randomTypes[randomIndex];
        }

        private void GenerateLayerDistances()
        {
            layerDistances = new List<float>();
            foreach (MapLayer layer in config.layers)
                layerDistances.Add(layer.DistanceFromPreviousLayer.GetValue());
        }
    }
}