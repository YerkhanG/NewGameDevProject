using System.Collections.Generic;
using persistence_system.model;
using UnityEngine;
using System.IO;
using map_encounter_system.map_system.data;
using map_encounter_system.map_system.data.node;
using Newtonsoft.Json;
using persistence_system.helpers;

namespace persistence_system.manager
{
    public class PersistenceManager : MonoBehaviour
    {
        public static PersistenceManager instance;
        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else 
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }
        public void SaveSceneData(Map saveData)
        {
            //Save map , playterstate
            var mapToSave = ToDTO(saveData);
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new Vector2Converter());
            string json =  JsonConvert.SerializeObject(mapToSave, settings);
            File.WriteAllText(Application.persistentDataPath + "/sceneSaveData.json", json);
            Debug.Log("Game Saved!");
        }

        public void SaveSessionData()
        {
            //unclokced stuff 
            
        }

        public void LoadSessionData()
        {
            
        }

        public Map LoadSceneData()
        {
            string path = Application.persistentDataPath + "/sceneSaveData.json";
            if (File.Exists(path))
            {
                string json =  File.ReadAllText(path);
                MapDTO loadedDto = JsonConvert.DeserializeObject<MapDTO>(json, new Vector2Converter());
                Debug.Log("checing the loaded stuff : " + loadedDto.allNodes);
                Map restoredMap = FromDTO(loadedDto);
                Debug.Log("Game Loaded!");
                return restoredMap;
            }
            else
            {
                return null;
            }
        }
        
        public Map FromDTO(MapDTO dto)
        {
            // Recreate Node objects from DTOs, but without connections yet
            var nodesByIdx = new List<Node>();
            foreach (var nodeDto in dto.allNodes)
            {
                var node = new Node(nodeDto.type, null, nodeDto.position ,nodeDto.gridPosition);
                node.isConnectedTo = nodeDto.isConnectedTo;
                // connections will be added later
                nodesByIdx.Add(node);
            }

            // Restore connections using indices
            for (int i = 0; i < dto.allNodes.Count; i++)
            {
                var nodeDto = dto.allNodes[i];
                var node = nodesByIdx[i];
                foreach (int connIdx in nodeDto.connectionIndices)
                {
                    var connectedNode = nodesByIdx[connIdx];
                    node.AddConnection(connectedNode);
                }
            }

            // Rebuild the 2D node list structure
            var nodeGrid = new List<List<Node>>();
            foreach (var rowIndices in dto.nodeGrid)
            {
                var row = new List<Node>();
                foreach (int idx in rowIndices)
                {
                    row.Add(nodesByIdx[idx]);
                }
                nodeGrid.Add(row);
            }

            return new Map(dto.configName, dto.bossNodeName, nodeGrid, dto.path);
        }
        public MapDTO ToDTO(Map map)
        {
            // Flatten the 2D nodes list and assign an index to each node
            var allNodes = new List<NodeDTO>();
            var indexMap = new Dictionary<Node, int>(); // map original Node object to its index

            // First pass: assign indices and create basic NodeDTOs
            foreach (var row in map.nodes)
            {
                foreach (var node in row)
                {
                    int idx = allNodes.Count;
                    indexMap[node] = idx;
                    allNodes.Add(new NodeDTO
                    {
                        type = node.type,
                        position = node.position,
                        connectionIndices = new List<int>(),
                        isConnectedTo = node.isConnectedTo,
                        gridPosition = node.gridPosition,
                    });
                }
            }

            // Second pass: fill connection indices
            foreach (var row in map.nodes)
            {
                foreach (var node in row)
                {
                    int nodeIdx = indexMap[node];
                    var dto = allNodes[nodeIdx];
                    foreach (var gridPos in node.connections2)
                    {
                        Node connectedNode = null;
                        foreach (var searchRow in map.nodes)
                        {
                            foreach (var n in searchRow)
                            {
                                if (n.gridPosition == gridPos)
                                {
                                    connectedNode = n;
                                    break;
                                }
                            }
                            if (connectedNode != null) break;
                        }
            
                        if (connectedNode != null)
                            dto.connectionIndices.Add(indexMap[connectedNode]);
                    }
                }
            }

            // Build nodeGrid: list of lists of indices (preserve original shape)
            var nodeGrid = new List<List<int>>();
            foreach (var row in map.nodes)
            {
                var rowIndices = new List<int>();
                foreach (var node in row)
                {
                    rowIndices.Add(indexMap[node]);
                }
                nodeGrid.Add(rowIndices);
            }

            return new MapDTO
            {
                nodeGrid = nodeGrid,
                allNodes = allNodes,
                path = map.path,
                bossNodeName = map.bossNodeName,
                configName = map.configName
            };
        }
}
}