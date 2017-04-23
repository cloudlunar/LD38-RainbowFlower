using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class Water : MonoBehaviour {

    //Our renderer that'll make the top of the water visible
    LineRenderer Body;

    //Our physics arrays
    float[] xpositions;
    float[] ypositions;
    float[] velocities;
    float[] accelerations;

    //Our meshes and colliders
    GameObject[] meshobjects;
    GameObject[] colliders;
    Mesh[] meshes;
    ParticleSystem[] splashs;
     ParticleSystem lastSplash;
    public GameObject splashGameObject;

    //Our particle system

    //The material we're using for the top of the water
    public Material mat;

    //The GameObject we're using for a mesh
    public GameObject watermesh;

    //All our constants
    const float springconstant = 0.02f;
    const float damping = 0.04f;
    const float spread = 0.05f;
    const float z = 0f;

    //The properties of our water
    float baseheight;
    float left;
    float bottom;
    

    void Start()
    {
        inWater = null;
        var cd=GetComponent<BoxCollider2D>();
        //(float Left, float Width, float Top, float Bottom)
        SpawnWater(cd.bounds.min.x,cd.bounds.size.x,cd.bounds.max.y+0.05f,cd.bounds.min.y);
        //Destroy(cd);
    }

    
    public void Splash(float xpos, float velocity)
    {
        //If the position is within the bounds of the water:
        if (xpos >= xpositions[0] && xpos <= xpositions[xpositions.Length-1])
        {
            //Offset the x position to be the distance from the left side
            xpos -= xpositions[0];

            //Find which spring we're touching
            int index = Mathf.RoundToInt((xpositions.Length-1)*(xpos / (xpositions[xpositions.Length-1] - xpositions[0])));

            //Add the velocity of the falling object to the spring
            velocities[index] += velocity;

            //Set the lifetime of the particle system.
            float lifetime = 0.33f + Mathf.Abs(velocity)*0.07f;

            //Set the splash to be between two values in Shuriken by setting it twice.
            if (lastSplash)
            {
                var psm = lastSplash.main;
                psm.startSpeed = 8 + 2 * Mathf.Pow(Mathf.Abs(velocity), 0.5f);
                psm.startSpeed = 9 + 2 * Mathf.Pow(Mathf.Abs(velocity), 0.5f);
                psm.startLifetime = lifetime;
            }
            //Set the correct position of the particle system.
            Vector3 position = new Vector3(xpositions[index],ypositions[index]-0.35f,transform.position.z);

            //This line aims the splash towards the middle. Only use for small bodies of water:
            Quaternion rotation = Quaternion.LookRotation(new Vector3(xpositions[Mathf.FloorToInt(xpositions.Length / 2)], baseheight + 8, 5) - position);

            //Create the splash and tell it to destroy itself.
            //GameObject splish = Instantiate(splash,position,rotation) as GameObject;
            //Destroy(splish, lifetime+0.3f);
            lastSplash = splashs[index];
            if (lastSplash&&!lastSplash.isPlaying)
            {
                lastSplash.transform.position = position;
                lastSplash.Play();
            }
        }
    }

    public void SpawnWater(float Left, float Width, float Top, float Bottom)
    {
        //Bonus exercise: Add a box collider to the water that will allow things to float in it.
        //gameObject.AddComponent<BoxCollider2D>();
        gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(Left + Width / 2, (Top + Bottom) / 2);
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(Width, Top - Bottom);
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;


        //Calculating the number of edges and nodes we have
        int edgecount = Mathf.RoundToInt(Width)*4;
        int nodecount = edgecount + 1;
        
        //Add our line renderer and set it up:
        Body = gameObject.AddComponent<LineRenderer>();
        Body.material = mat;
        Body.material.renderQueue = 1000;
        Body.positionCount=(nodecount);
        Body.startWidth=Body.endWidth=0.1f;

        //Declare our physics arrays
        xpositions = new float[nodecount];
        ypositions = new float[nodecount];
        velocities = new float[nodecount];
        splashs = new ParticleSystem[nodecount];
        accelerations = new float[nodecount];
        
        //Declare our mesh arrays
        meshobjects = new GameObject[edgecount];
        meshes = new Mesh[edgecount];
        colliders = new GameObject[edgecount];

        //Set our variables
        baseheight = Top;
        bottom = Bottom;
        left = Left;

        //For each node, set the line renderer and our physics arrays
        for (int i = 0; i < nodecount; i++)
        {
            ypositions[i] = Top;
            xpositions[i] = Left + Width * i / edgecount;
            Body.SetPosition(i, new Vector3(xpositions[i], Top, z));
            accelerations[i] = 0;
            velocities[i] = 0;
        }

        //Setting the meshes now:
        for (int i = 0; i < edgecount; i++)
        {
            //Make the mesh
            meshes[i] = new Mesh();

            //Create the corners of the mesh
            Vector3[] Vertices = new Vector3[4];
            Vertices[0] = new Vector3(xpositions[i], ypositions[i], z);
            Vertices[1] = new Vector3(xpositions[i + 1], ypositions[i + 1], z);
            Vertices[2] = new Vector3(xpositions[i], bottom, z);
            Vertices[3] = new Vector3(xpositions[i+1], bottom, z);

            //Set the UVs of the texture
            Vector2[] UVs = new Vector2[4];
            UVs[0] = new Vector2(0, 1);
            UVs[1] = new Vector2(1, 1);
            UVs[2] = new Vector2(0, 0);
            UVs[3] = new Vector2(1, 0);

            //Set where the triangles should be.
            int[] tris = new int[6] { 0, 1, 3, 3, 2, 0};

            //Add all this data to the mesh.
            meshes[i].vertices = Vertices;
            meshes[i].uv = UVs;
            meshes[i].triangles = tris;

            //Create a holder for the mesh, set it to be the manager's child
            meshobjects[i] = Instantiate(watermesh,Vector3.zero,Quaternion.identity) as GameObject;
            meshobjects[i].GetComponent<MeshFilter>().mesh = meshes[i];
            meshobjects[i].transform.parent = transform;
            var ls=meshobjects[i].transform.localScale;ls.z=0;
            meshobjects[i].transform.localScale=ls;
            //Create our colliders, set them be our child
            colliders[i] = new GameObject();
            colliders[i].name = "Trigger";
            colliders[i].AddComponent<BoxCollider2D>();
            colliders[i].transform.parent = transform;

            //Set the position and scale to the correct dimensions
            colliders[i].transform.position = new Vector3(Left + Width * (i + 0.5f) / edgecount, 0.5f*(Top+Bottom), 0);
            colliders[i].transform.localScale = new Vector3(Width / edgecount/transform.localScale.x, 1, 0);

            //Add a WaterDetector and make sure they're triggers
            colliders[i].GetComponent<BoxCollider2D>().isTrigger = true;
            colliders[i].AddComponent<WaterDetector>();
            var gb = Instantiate(splashGameObject,transform.position,transform.rotation) as GameObject;
            lastSplash =splashs[i]= gb.GetComponent<ParticleSystem>();

        }

        
        
        
    }

    //Same as the code from in the meshes before, set the new mesh positions
    void UpdateMeshes()
    {
        for (int i = 0; i < meshes.Length; i++)
        {

            Vector3[] Vertices = new Vector3[4];
            Vertices[0] = new Vector3(xpositions[i], ypositions[i], z);
            Vertices[1] = new Vector3(xpositions[i+1], ypositions[i+1], z);
            Vertices[2] = new Vector3(xpositions[i], bottom, z);
            Vertices[3] = new Vector3(xpositions[i+1], bottom, z);

            meshes[i].vertices = Vertices;
        }
    }
    SpriteRenderer spr;
    //Called regularly by Unity
    private void Update()
    {
        UpdateStat();
    }
    void FixedUpdate()
    {
        if (spr == null) spr = GetComponent<SpriteRenderer>();
        if (!spr.isVisible) return;
        //Here we use the Euler method to handle all the physics of our springs:
        for (int i = 0; i < xpositions.Length ; i++)
        {
            float force = springconstant * (ypositions[i] - baseheight) + velocities[i]*damping ;
            accelerations[i] = -force;
            ypositions[i] += velocities[i];
            velocities[i] += accelerations[i];
            Body.SetPosition(i, new Vector3(xpositions[i], ypositions[i], z));
        }

        //Now we store the difference in heights:
        float[] leftDeltas = new float[xpositions.Length];
        float[] rightDeltas = new float[xpositions.Length];

        //We make 8 small passes for fluidity:
        for (int j = 0; j < 8; j++)
        {
            for (int i = 0; i < xpositions.Length; i++)
            {
                //We check the heights of the nearby nodes, adjust velocities accordingly, record the height differences
                if (i > 0)
                {
                    leftDeltas[i] = spread * (ypositions[i] - ypositions[i-1]);
                    velocities[i - 1] += leftDeltas[i];
                }
                if (i < xpositions.Length - 1)
                {
                    rightDeltas[i] = spread * (ypositions[i] - ypositions[i + 1]);
                    velocities[i + 1] += rightDeltas[i];
                }
            }

            //Now we apply a difference in position
            for (int i = 0; i < xpositions.Length; i++)
            {
                if (i > 0)
                    ypositions[i-1] += leftDeltas[i];
                if (i < xpositions.Length - 1)
                    ypositions[i + 1] += rightDeltas[i];
            }
            /*/Clamp
            for (int i = 0; i < xpositions.Length; i++)
            {
                if(ypositions[i]>baseheight*(1+i*1f))
                    ypositions[i]=baseheight*(1+i*1f);
                if(ypositions[ xpositions.Length-i-1]>baseheight*(1+i*1f))
                    ypositions[ xpositions.Length-i-1]=baseheight*(1+i*1f);
                    
            }*/
            ypositions[0]=ypositions[ xpositions.Length-1]=baseheight;
        }
        
        //Finally we update the meshes to reflect this
        UpdateMeshes();
	}
    public static Creature inWater;
    Sequence seq;
    void OnTriggerExit2D(Collider2D other)
    {
      //  print(other.name+"离开水中");
        var hm=other.GetComponent<PlayerControl>();
        if(other.bounds.center.y< GetComponent<Collider2D>().bounds.center.y)return;
        if (hm != null)
        {
            if(seq!=null)seq.Kill();
            seq = DOTween.Sequence();
            
            seq.AppendInterval(0.2f).AppendCallback(() =>
            {
                inWater = null;
                if (Creature.player.abilities.Contains("Swim"))
                    ControlMsgUI.Hide("↑ Floating Up ↓ Dive");
            });
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        
              //  print(other.name+"进入水中");
        var hm = other.GetComponent<PlayerControl>();
        if (hm != null)
        {
            if (seq != null) seq.Kill();
            inWater = hm.GetComponent<Creature>();
            if (Creature.player.abilities.Contains("Swim"))
                ControlMsgUI.Show("↑ Floating Up ↓ Dive");
        }
    }
    float dmgcd;
    private void UpdateStat()
    {
        var x = inWater;
        if (x == null) { return; }
        
        // print("##" + dmgcd);
        if (inWater.abilities.Contains("Swim"))
            x.usingST += 0.05f;
        else
        {
            x.usingST +=0.5f;

            x.GetComponent<Rigidbody2D>().AddForce(Vector3.down *1.5f);
        }
        if ( x.ST < 1)
        {
            x.GetComponent<Rigidbody2D>().AddForce(Vector3.down *16);
            dmgcd+= Time.deltaTime;
            if (dmgcd > 0.3)
            {
                dmgcd = 0;
                x.HP = Mathf.Clamp(x.HP - 0.5f, 0, 10);
            }
        }
        else
        {
            dmgcd =0.2f;
        }
        
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue * 0.4f;
        Gizmos.DrawCube(transform.position, GetComponent<BoxCollider2D>().bounds.size);

    }
#endif

}
