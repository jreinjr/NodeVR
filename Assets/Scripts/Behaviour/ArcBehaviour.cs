using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace NodeVR
{
    public class ArcBehaviour : MonoBehaviour, ISelectable, IHasInfo, IHasActions
    {
        public Arc arc;

        public LineRenderer lineRenderer;
        public Texture lineTexture;
        public NodeBehaviour fromNode;
        public NodeBehaviour toNode;
        public LineRenderer selectionLine;

        public bool IsReversed { get; protected set; }

        public bool IsSelected { get; protected set; }
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

            var offset = lineRenderer.material.GetTextureOffset("_MainTex");
            var increment = Time.deltaTime * Vector2.left;
            lineRenderer.material.SetTextureOffset("_MainTex", offset + increment);

            var scaleFactor = lineAspect / lineWidth * (IsReversed ? -1 : 1);
            var scale = new Vector2(scaleFactor, 1);

            lineRenderer.material.SetTextureScale("_MainTex", scale);
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

        public void Select()
        {
            IsSelected = true;
            selectionLine.gameObject.SetActive(true);
        }

        public void Deselect()
        {
            IsSelected = false;
            selectionLine.gameObject.SetActive(false);
        }

        public List<(string, string)> ReportInfo()
        {
            var report = new List<(string, string)>();

            report.Add(("Psi Through Per Sec", maxPsiThruPerSec.ToString()));

            return report;
        }

        public List<(string, UnityAction)> ReportActions()
        {
            var report = new List<(string, UnityAction)>();

            report.Add(("Reverse Path Direction", new UnityAction(ReversePathDirection)));
            report.Add(("UpgradePsiThruPerSec", new UnityAction(UpgradePsiThroughPerSec)));

            return report;
        }
    }
}