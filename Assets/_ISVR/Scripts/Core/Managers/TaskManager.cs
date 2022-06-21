using ISVR.Core.Devices;
using ISVR.Core.Marks;
using System.Collections.Generic;
using UnityEngine;

namespace ISVR.Core.Managers {

    public class TaskManager : MonoBehaviour {

        [SerializeField] private Color correctMarkColor;
        [SerializeField] private float marksSearchDistance;

        public int CalculatePredictResult(List<Wiretapper> wiretappers, List<Mark> marks) {
            int count = 0;
            foreach (Wiretapper wiretapper in wiretappers) {
                wiretapper.gameObject.layer = 7;
                var meshRenderer = wiretapper.GetComponent<MeshRenderer>();
                if (!meshRenderer.enabled) {
                    meshRenderer.enabled = true;
                }
                Mark mark = GetNearestMark(marks, wiretapper.transform.position);
                if (mark != null) {
                    count++;
                    mark.MarkAsCorrect(correctMarkColor);
                    wiretapper.gameObject.layer = 8;
                }
            }
            return count;
        }

        public int CalculatePredictResult(List<Electronic> electronics, List<Mark> marks) {
            int count = 0;
            foreach (Electronic electronic in electronics) {
                electronic.gameObject.layer = 7;
                var meshRenderer = electronic.GetComponent<MeshRenderer>();
                if (!meshRenderer.enabled) {
                    meshRenderer.enabled = true;
                }
                Mark mark = GetNearestMark(marks, electronic.transform.position);
                if (mark != null) {
                    count++;
                    mark.MarkAsCorrect(correctMarkColor);
                    electronic.gameObject.layer = 8;
                }
            }
            return count;
        }

        private Mark GetNearestMark(List<Mark> marks, Vector3 position) {
            Mark result = null;
            float maxDistance = Mathf.Infinity;
            float distance;
            foreach (Mark mark in marks) {
                distance = Vector3.Distance(mark.transform.position, position);
                if (distance < maxDistance) {
                    maxDistance = distance;
                    result = mark;
                }
            }
            if (maxDistance < marksSearchDistance) {
                return result;
            }
            return null;
        }
    }
}