using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine; 

public class BlockController : MonoBehaviour
{
    [SerializeField] ParticleSystem popParticle;
    [SerializeField] BitController[] bitControllers;
    protected int openBit;
    int openLeg;
    public int OpenBit { get { return openBit; } }

    [System.Obsolete]
    public void Initialize(Vector2 position, int openLeg) {
        transform.position = position + new Vector2(0, 8);
        transform.DOMove(position, 1f);
        openBit = Random.Range(0, 4);
        this.openLeg = openLeg;
        for (int i = 0; i < 4; i++) {
            bitControllers[i].gameObject.SetActive(this.openLeg == i);
            bitControllers[i].Initialize(openBit);
        }
        switch (openBit) {
            case 0:
                popParticle.startColor = Color.red;
                break;
            case 1:
                popParticle.startColor = Color.blue;
                break;
            case 2:
                popParticle.startColor = Color.yellow;
                break;
            case 3:
                popParticle.startColor = Color.green;
                break;
            default:
                break;
        }
    }
    public void Pop() { 
        bitControllers[openLeg].Pop();
        popParticle.Play();
        //StartCoroutine(PopCoroutine()); 
    }


    IEnumerator PopCoroutine() {
        yield return new WaitForSeconds(.5f);
    }
    public void Move(Vector2 pos, bool rotate = false) {
        transform.DOMove(pos, .5f).SetEase(Ease.InCirc); 
        if(rotate)
        transform.DORotate(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z + 90 * UnityEngine.Random.Range(0,3)), .5f);
    }
}
