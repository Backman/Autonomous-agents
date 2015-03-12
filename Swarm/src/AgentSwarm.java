/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */


import java.util.*;

/**
 * http://www.kfish.org/boids/pseudocode.html
 * 
 * @author admin
 */
public class AgentSwarm 
{
    public int NO_AGENTS = 50;
    public Vector<Agent> swarm;
    public Vector<Agent> obstacles;
    private static AgentSwarm instance;
    
    public int w = 430;
    public int h = 400;
    
    public double goalX = w / 2.0;
    public double goalY = h / 2.0;
    
    public static AgentSwarm getInstance()
    {
        if (instance == null)
        {
            instance = new AgentSwarm();
        }
        return instance;
    }
    
    private AgentSwarm()
    {
        generateRandom();
    }
    
    public void generateRandom()
    {
        obstacles = new Vector<Agent>();
        obstacles.add(new Agent(100, 100, 0));
        obstacles.add(new Agent(150, 150, 0));
        obstacles.add(new Agent(250, 250, 0));
        obstacles.add(new Agent(300, 300, 0));
        
        swarm = new Vector<Agent>();
        for (int i = 0; i < NO_AGENTS; i++)
        {
            double[] pos = genRandomPosition();
            swarm.add(new Agent(pos[0], pos[1], i));
        }
    }
    
    public double[] genRandomPosition()
    {
        double[] pos = new double[2];
        
        int rndX = 10 + (int)(Math.random() * (double)(w - 20));
        int rndY = 10 + (int)(Math.random() * (double)(h - 20));
        
        for (Agent o:obstacles)
        {
            Agent tmp = new Agent(rndX, rndY, 0);
            if (getDistance(tmp, o) <= 30)
            {
                return genRandomPosition();
            }
        }
        
        pos[0] = rndX;
        pos[1] = rndY;
        
        return pos;
    }
    
    public void setGoal(double gX, double gY)
    {
        this.goalX = gX;
        this.goalY = gY; 
    }
    
    public void update()
    {
        for (Agent a:swarm)
        {
            a.update();
        }
    }
    
    //Cohesion rule - steer towards average position
    public void applyAveragePosition(Agent a1)
    {
        double totDX = 0;
        double totDY = 0;
        for (Agent a:swarm)
        {
            if (a.id != a1.id)
            {
                totDX += a.x;
                totDY += a.y;
            }
        }
        
        totDX = totDX / (double)(swarm.size() - 1);
        totDY = totDY / (double)(swarm.size() - 1);
        
        a1.dX += (totDX - a1.x) / 100.0;
        a1.dY += (totDY - a1.y) / 100.0;
    }
    
    //Goal rule - steer towards the goal
    public void applyGoal(Agent a1)
    {
        a1.dX += (goalX - a1.x) / 100.0;
        a1.dY += (goalY - a1.y) / 100.0;
    }
    
    //Alignment rule - steer towards average heading
    public void applyAverageHeading(Agent a1)
    {
        double totDX = 0;
        double totDY = 0;
        for (Agent a:swarm)
        {
            if (a.id != a1.id)
            {
                totDX += a.dX;
                totDY += a.dY;
            }
        }
        
        double nDX = totDX / (double)(swarm.size() - 1);
        double nDY = totDY / (double)(swarm.size() - 1);
        
        a1.dX += (nDX - a1.dX) / 8.0;
        a1.dY += (nDY - a1.dY) / 8.0;
    }
    
    //Separation rule - steer to avoid crowding local flockmates
    public void applySeparation(Agent a1)
    {
        double totDX = 0;
        double totDY = 0;
        
        double detectionLimit = 10.0;
        
        //Find agents that are too close
        for (Agent a:swarm)
        {
            if (a.id != a1.id)
            {
                double d = getDistance(a1, a);
                if (d <= detectionLimit)
                {
                    totDX -= (a.x - a1.x);
                    totDY -= (a.y - a1.y);
                }
            }
        }
        
        a1.dX += totDX;
        a1.dY += totDY;
        
        //Obstacles
        totDX = 0.0;
        totDY = 0.0;
        for (Agent o:obstacles)
        {
            double d = getDistance(a1, o);
            if (d <= 26 + detectionLimit)
            {
                totDX -= (o.x - a1.x);
            }
        }
        
        a1.dX += totDX * 8.0;
        a1.dY += totDY * 8.0;
    }
    
    private double getDistance(Agent a1, Agent a2)
    {
        double a = (double)a1.x - (double)a2.x;
        double b = (double)a1.y - (double)a2.y;
        
        return Math.sqrt(Math.pow(a, 2) + Math.pow(b, 2));
    }
}
