using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace NodeVR
{
    [RequireComponent(typeof(Selectable))]
    public class ArcBehaviour : MonoBehaviour, IHasStats, IHasUpgrades
    {
        public Arc arc;

        public Selectable selectable;

        public LineRenderer selectionLine;
        public LineRenderer lineRenderer;
        public Texture lineTexture;
        public NodeBehaviour fromNode;
        public NodeBehaviour toNode;

        public bool IsReversed { get; protected set; }
        public bool IsFlowing { get; protected set; }

        //public bool IsActive { get; set; }
        public float lineAspect = 0.75f;
        public float lineWidth = 0.2f;

        public int maxPsiThruPerSec = 0;
        public int currentPsiThruPerSec = 0;
        public float boxColliderRadius = .25f;

        bool IsInitialized = false;
        private void Start()
        {
            if (!IsInitialized && fromNode != null && toNode != null)
                Initialize(fromNode, toNode);
        }

        public void Initialize(NodeBehaviour fromNode, NodeBehaviour toNode)
        {
            IsFlowing = false;

            this.fromNode = fromNode;
            this.toNode = toNode;

            Vector3 from = fromNode.transform.position;
            Vector3 to = toNode.transform.position;

            transform.rotation = Quaternion.LookRotation(to - from, Vector3.up);

            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.SetPositions(new Vector3[] { from, to });

            selectionLine.SetPositions(new Vector3[] { from, to });

            AddColliderToLine(from, to);

            UpdateRenderer();

            IsInitialized = true;
        }


        public void Tick()
        {
            UpdateRenderer();
        }

        private void AddColliderToLine(Vector3 startPos, Vector3 endPos)
        {
            BoxCollider col = gameObject.AddComponent<BoxCollider>();
            float lineLength = Vector3.Distance(startPos, endPos);
            col.size = new Vector3(boxColliderRadius, boxColliderRadius, lineLength);
        }

        private void UpdateRenderer()
        {
            var plainOrDirectionTex = IsFlowing ? lineTexture : null;
            lineRenderer.material.SetTexture("_MainTex", plainOrDirectionTex);

            lineWidth = IsFlowing ? maxPsiThruPerSec * 0.05f : 0.025f;

            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;

            // Using shadergraph
            Vector2 offset = lineRenderer.material.GetVector("_Offset");
            var increment = Time.deltaTime * Vector2.left;

            // Using shadergraph
            lineRenderer.material.SetVector("_Offset", offset + increment);

            var scaleFactor = lineAspect / lineWidth * (IsReversed ? -1 : 1);
            var scale = new Vector2(scaleFactor, 1);

            // Using shadergraph
            lineRenderer.material.SetTextureScale("_Tiling", scale);

        }

        public void ReversePathDirection()
        {
            var temp = fromNode;
            fromNode = toNode;
            toNode = temp;
            IsReversed = !IsReversed;
        }

        public void UpgradePsiThroughPerSec()
        {
            maxPsiThruPerSec++;
            IsFlowing = true;
        }

        public List<(string, string)> ReportStats()
        {
            var report = new List<(string, string)>();

            report.Add(("Psi Through Per Sec", maxPsiThruPerSec.ToString()));

            return report;
        }

        public List<(string, UnityAction)> ReportAvailableUpgrades()
        {
            var report = new List<(string, UnityAction)>();

            report.Add(("Reverse Path Direction", new UnityAction(ReversePathDirection)));
            report.Add(("UpgradePsiThruPerSec", new UnityAction(UpgradePsiThroughPerSec)));

            return report;
        }
    }
}