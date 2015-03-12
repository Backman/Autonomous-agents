/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */


import java.awt.Color;
import javax.vecmath.Vector2d;

/**
 *
 * @author admin
 */
public class Agent
{
    public double x;
    public double y;
    public Color color;
    
    public double dX = 0;
    public double dY = 0;
    public double speedLimit = 1.0;
    
    public int id;
    
    public Agent(double x, double y, int id)
    {
        this.x = x;
        this.y = y;
        this.id = id;
        rndColor();
        
        dX = Math.random() * 2.0 - 1.0;
        dY = Math.random() * 2.0 - 1.0;
    }
    
    private void rndColor()
    {
        double rnd = Math.random();
        if (rnd >= 0.8)
        {
            color = Color.red;
            return;
        }
        else if(rnd >= 0.6)
        {
            color = Color.blue;
            return;
        }
        else if(rnd >= 0.4)
        {
            color = Color.green;
            return;
        }
        else if(rnd >= 0.2)
        {
            color = Color.magenta;
            return;
        }
        else
        {
            color = Color.orange;
            return;
        }
    }
    
    public void update()
    {
        AgentSwarm swarm = AgentSwarm.getInstance();
        swarm.applyAveragePosition(this);
        swarm.applySeparation(this);
        swarm.applyAverageHeading(this);
        swarm.applyGoal(this);
        
        //Apply speed limit
        if (Math.abs(dX) > speedLimit)
        {
            dX = (dX / Math.abs(dX)) * speedLimit;
        }
        if (Math.abs(dY) > speedLimit)
        {
            dY = (dY / Math.abs(dY)) * speedLimit;
        }
        
        double newX = x + dX;
        double newY = y + dY;
        
        //Apply some error checks
        if (newX < 5.0) newX = 5.0;
        if (newX > swarm.w - 5.0) newX = swarm.w - 5.0;
        if (newY < 5.0) newY = 5.0;
        if (newY > swarm.h - 5.0) newY = swarm.h - 5.0;
        
        x = newX;
        y = newY;
    }
}
