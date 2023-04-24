using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{

    private Animator a = null;

    public RuntimeAnimatorController sWalk, sKnife;


    // Start is called before the first frame update
    void Start()
    {
        a = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //a.runtimeAnimatorController = sKnife;
    }

    public IEnumerator KnifeAnim() {
        a.runtimeAnimatorController = sKnife;

        yield return new WaitForSeconds(1);

        a.runtimeAnimatorController = sWalk;
    }
}
