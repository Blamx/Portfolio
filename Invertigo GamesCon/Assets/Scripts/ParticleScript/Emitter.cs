using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
using UnityEngine;

public class Emitter : MonoBehaviour
{

    public GameObject particlePrefab;

    public int numOfParts;

    private int parent;

    [DllImport("Particles")]
    public static extern int initSystem();

    [DllImport("Particles")]
    public static extern void initialize(Particles[] p, int parent, int Size, int MaxDistance, int MaxOutVelo, float MaxLifeTime, float SpawnRate, float SpawnTime, float gravX, float gravY, float gravZ, float dt);

    [DllImport("Particles")]
    public static extern void updatePOS(float x, float y, float z, int parent);

    [DllImport("Particles")]
    public static extern void updatePPOS(Particles[] p, float dt, int parent);

    [DllImport("Particles")]
    public static extern void end(int parent);

    [StructLayout(LayoutKind.Sequential)]
    public struct Vec
    {
        public float x, y, z;
    };
    [StructLayout(LayoutKind.Sequential)]
    public struct Particles
    {
        float Drag;
        public Vec Pos;
        public Vec Vel;
        public Vec Accel;
        public float LifeTime;
        public int active;
    };

    public GameObject[] g_Parts;
    public Particles[] parts;

    public float spawnRate, spawntime, life;
    private float emitterlife;

    // Use this for initialization
    void Start()
    {
        g_Parts = new GameObject[25];
        parts = new Particles[25];
        parent = initSystem();

        if (parent == -1)
        {

            Destroy(this.gameObject);
        }
        if (parent >= 0)
        {
            initialize(parts, parent, parts.Length, 10, 40, life, spawnRate, spawntime,
              0, 0.9f, 0
              , Time.deltaTime);
        }
        emitterlife = spawntime + life;
    }
    // Update is called once per frame
    void Update()
    {

        emitterlife -= Time.deltaTime;

        if (emitterlife < 0)
        {
            end(parent);
            Destroy(this.gameObject);
        }

        updatePOS(transform.position.x, transform.position.y, transform.position.z, parent);

        for (int i = 0; i < parts.Length; i++)
        {
            if (g_Parts[i] == null)
            {
                if (parts[i].active == 1)
                {

                    g_Parts[i] = Instantiate<GameObject>(particlePrefab, transform);
                    g_Parts[i].transform.position = getPos(i);
                }
            }
            else
            {
                if (parts[i].active == 1)
                {
                    g_Parts[i].transform.position = getPos(i);
                }
                else
                {
                    g_Parts[i].gameObject.GetComponent<Particle>().die = true;
                }
            }

        }

        updatePPOS(parts, Time.deltaTime, parent);


    }
    public Vector3 getPos(int pos)
    {
        return new Vector3(parts[pos].Pos.x, parts[pos].Pos.y, parts[pos].Pos.z);
    }
}
