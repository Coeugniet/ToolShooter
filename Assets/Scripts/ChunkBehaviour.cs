using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkBehaviour : Poolable
{

    private Camera myCamera;
    private Transform self;

    public ChunkDataProfile data;

    public override void Initialise() {
        myCamera = Camera.main;
        self = transform;
        self.position = myCamera.transform.position + new Vector3(0 , -4.5f, 0);
        self.position = new Vector3(self.position.x, self.position.y, 0);
    }

    public override void OnPooled() {
        return;
    }

    private void Update() {
        transform.Translate(new Vector3(-1, 0, 0) * data.speed * Time.deltaTime);
    }
}
