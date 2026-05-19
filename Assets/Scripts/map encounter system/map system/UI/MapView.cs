using System;
using System.Collections.Generic;
using System.Linq;
using map_encounter_system.map_system.data;
using map_encounter_system.map_system.data.node;
using map_encounter_system.map_system.manager;
using map_encounter_system.map_system.UI;
using map_encounter_system.map_system.UI.scrolling;
using TreeEditor;
using UnityEditor.Toolbars;
using UnityEngine;

namespace map_encounter_system.map_system
{
    public class MapView : MonoBehaviour
    {
        public GameObject nodePrefab;
        public Node lastBeatenNode;
        public Transform containerTransform;
        public List<NodeView> nodeViews = new List<NodeView>();
        [SerializeField] private GameObject linePrefab;
        public Map map;
        private Camera cam;
        [Tooltip("Offset of the start/end nodes of the map from the edges of the screen")]
        public float orientationOffset;
        [Range(3, 10)]
        public int linePointsCount = 10;
        [Tooltip("Distance from the node till the line starting point")]
        public float offsetFromNodes = 0.5f;
        
        protected readonly List<LineConnection> lineConnections = new List<LineConnection>();
        
        private void Awake()
        {
            cam = Camera.main;
        }
        public void ShowMap(Map map)
        {
            this.map = map;
            CreateNodes(map.nodes);
            DrawLines();
            SetOrientation();
            /*SetAttainableNodes();*/
        }

        /*private void SetAttainableNodes()
        {
            if (lastBeatenNode == null)
            {
                foreach (var view in nodeViews)
                {
                    // Assuming layer index is stored in the Y coordinate of Vector2Int
                    view.SetInteractable(view.Node.position.y == 0);
                }
            }
            else
            {
                // Lock everything first
                foreach (var view in nodeViews) view.SetInteractable(false);

                // Unlock ONLY the nodes connected to the one we just beat
                foreach (Node connection in lastBeatenNode.connections)
                {
                    // Find the view that matches this data node and unlock it
                    NodeView view = nodeViews.Find(v => v.Node == connection);
                    if (view != null) view.SetInteractable(true);
                }
            }
        }*/

        protected void SetOrientation()
        {
            ScrollNonUI scrollNonUi = containerTransform.GetComponent<ScrollNonUI>();
            float span = MapManager.instance.map.DistanceBetweenFirstAndLastLayers();
            /*NodeView bossNode = nodeViews.FirstOrDefault(node => node.Node.type == TreeEditorHelper.NodeType.Boss);*/
            Debug.Log("Map span in set orientation: " + span + " camera aspect: " + cam.aspect);

            // setting first parent to be right in front of the camera first:
            containerTransform.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, 0f);
            float offset = orientationOffset;
           
            if (scrollNonUi != null)
            {
                scrollNonUi.yConstraints.max = 0;
                scrollNonUi.yConstraints.min = -(span + 2f * offset);
            }
            containerTransform.transform.localPosition += new Vector3(0, offset, 0);
        }

        public void CreateNodes(List<List<Node>> nodes)
        {
            foreach (List<Node> nodeList in nodes)
            {
                foreach (Node node in nodeList)
                {
                    NodeView nodeView = CreateMapNode(node);
                    nodeViews.Add(nodeView);   
                }
            }
        }

        public void DrawLines()
        {
            foreach (NodeView nodeView in nodeViews)
            {
                Debug.Log("connections of a node about to be drawn: " + nodeView.Node.connections.Count);
                foreach (Node targetNode in  nodeView.Node.connections)
                {
                    AddLineConnection(nodeView, GetNode(targetNode));
                }
            }
        }

        private NodeView GetNode(Node targetNode)
        {
            return nodeViews.Find(v => v.Node == targetNode);
        }

        private void AddLineConnection(NodeView from, NodeView to)
        {
            if (linePrefab == null) return;

            GameObject lineObject = Instantiate(linePrefab, containerTransform);
            LineRenderer lineRenderer = lineObject.GetComponent<LineRenderer>();
            Vector3 fromPoint = from.transform.position +
                                (to.transform.position - from.transform.position).normalized * offsetFromNodes;

            Vector3 toPoint = to.transform.position +
                              (from.transform.position - to.transform.position).normalized * offsetFromNodes;
            lineObject.transform.position = fromPoint;
            lineRenderer.useWorldSpace = true;
            
            lineRenderer.positionCount = linePointsCount;
            for (int i = 0; i < linePointsCount; i++)
            {
                Vector3 worldPosition = Vector3.Lerp(fromPoint, toPoint, (float)i / (linePointsCount - 1));
                lineRenderer.SetPosition(i, worldPosition);
            }

            /*DottedLineRenderer dottedLine = lineObject.GetComponent<DottedLineRenderer>();
            if (dottedLine != null) dottedLine.ScaleMaterial();*/

            lineConnections.Add(new LineConnection(null, lineRenderer, from, to));
        }
        private NodeView CreateMapNode(Node node)
        {
            GameObject nodeObject = Instantiate(nodePrefab, node.position ,  Quaternion.identity , containerTransform);
            NodeView nodeView = nodeObject.GetComponent<NodeView>();
            nodeView.Initialize(node);
            return nodeView;
        }
    }
}