/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */


import javax.swing.*;
import java.awt.*;
import java.util.*;

/**
 *
 * @author admin
 */
public class WorldPanel  extends JPanel
{
    private AgentSwarm swarm;
    
    public WorldPanel()
    {
        swarm = AgentSwarm.getInstance();
        this.setPreferredSize(new Dimension(430, 400));
    }
    
    public void paint(Graphics gn) 
    {
        Graphics2D g = (Graphics2D)gn;
        
        g.setColor(Color.white);
        g.fillRect(0, 0, this.getWidth(), this.getHeight());
        
        //Draw the obstacles
        g.setColor(Color.black);
        for (Agent o:swarm.obstacles)
        {
            g.fillOval((int)o.x - 25, (int)o.y - 25, 50, 50);
        }
        
        //Display goal
        g.setColor(Color.black);
        g.drawLine((int)swarm.goalX - 2, (int)swarm.goalY, (int)swarm.goalX + 2, (int)swarm.goalY);
        g.drawLine((int)swarm.goalX, (int)swarm.goalY - 2, (int)swarm.goalX, (int)swarm.goalY + 2);
        
        for (Agent a:swarm.swarm)
        {
            g.setColor(a.color);
            g.fillOval((int)Math.round(a.x) - 3, (int)Math.round(a.y) - 3, 7, 7);
        }
    }
}
