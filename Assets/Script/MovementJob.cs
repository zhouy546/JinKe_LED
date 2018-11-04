using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using Unity.Entities;
using Unity.Transforms;


public struct ICD_MoveSpeed : IComponentData {
    public float MoveSpeed;
}

public struct ICD_MoveHeight : IComponentData {
    public float dis;
}

public struct ICD_YDirection : IComponentData {

}

public class MovementJob : JobComponentSystem {
    private struct moveJob : IJobProcessComponentData<ICD_MoveHeight, ICD_MoveSpeed, Position>
    {

        public float DeltaTime;
     
        public void Execute(ref ICD_MoveHeight MoveHieght, ref ICD_MoveSpeed speed, ref Position pos)
        {



                pos.Value.y += speed.MoveSpeed * DeltaTime;
                if (pos.Value.y > MoveHieght.dis|| pos.Value.y < -MoveHieght.dis)
                {
                speed.MoveSpeed *= -1;
                }

        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var Job = new moveJob
        {
            DeltaTime = Time.deltaTime,
        };

        return Job.Schedule(this, inputDeps);
    }

}
