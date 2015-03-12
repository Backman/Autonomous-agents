
import javax.swing.*;
import java.awt.*;

/**
 *
 * @author Johan Hagelbäck
 */
public class PFpanel extends JPanel
{
    private World world;
    public boolean showGrid = false;
    
    public PFpanel(World world)
    {
        this.setPreferredSize(new Dimension(world.w * world.cellW + 1, world.h * world.cellW + 1));
        this.world = world;
    }
    
    public void paint(Graphics gn) 
    {
        Graphics2D g = (Graphics2D)gn;
        
        g.setColor(Color.black);
        g.fillRect(0, 0, this.getWidth()-1, this.getHeight()-1);
        
        //Paint PF
        for (int x = 0; x < world.w; x++)
        {
            for (int y = 0; y < world.h; y++)
            {
                g.setColor(getColor(world.totalPF[x][y]));
                g.fillRect(x * world.cellW, y * world.cellW, world.cellW, world.cellW);
                //Paint optional grid
                if (showGrid)
                {
                    g.setColor(Color.black);
                    g.drawRect(x * world.cellW, y * world.cellW, world.cellW, world.cellW);
                }
            }
        }
        
        //Enemies
        for (WObject o:world.enemies)
        {
            g.setColor(new Color(247, 44, 30));
            g.fillOval(o.x * world.cellW + 1, o.y * world.cellW + 1, world.cellW - 2, world.cellW - 2);
            g.setColor(new Color(94, 16, 10));
            g.drawOval(o.x * world.cellW + 1, o.y * world.cellW + 1, world.cellW - 2, world.cellW - 2);
        }
        
        //Evade
        for (WObject o:world.evade)
        {
            g.setColor(new Color(247, 44, 30));
            g.fillOval(o.x * world.cellW + 1, o.y * world.cellW + 1, world.cellW - 2, world.cellW - 2);
            g.setColor(new Color(94, 16, 10));
            g.drawOval(o.x * world.cellW + 1, o.y * world.cellW + 1, world.cellW - 2, world.cellW - 2);
        }
        
        //Own
        for (WObject o:world.own)
        {
            g.setColor(new Color(24, 242, 217));
            g.fillOval(o.x * world.cellW + 1, o.y * world.cellW + 1, world.cellW - 2, world.cellW - 2);
            g.setColor(new Color(7, 82, 73));
            g.drawOval(o.x * world.cellW + 1, o.y * world.cellW + 1, world.cellW - 2, world.cellW - 2);
        }
        
        //Goals
        for (WObject o:world.goals)
        {
            g.setColor(new Color(230, 30, 216));
            g.fillOval(o.x * world.cellW + 1, o.y * world.cellW + 1, world.cellW - 2, world.cellW - 2);
            g.setColor(new Color(87, 10, 82));
            g.drawOval(o.x * world.cellW + 1, o.y * world.cellW + 1, world.cellW - 2, world.cellW - 2);
        }
        
        //Agents
        for (WObject o:world.agents)
        {
            g.setColor(new Color(69, 245, 34));
            g.fillOval(o.x * world.cellW + 1, o.y * world.cellW + 1, world.cellW - 2, world.cellW - 2);
            g.setColor(new Color(24, 87, 11));
            g.drawOval(o.x * world.cellW + 1, o.y * world.cellW + 1, world.cellW - 2, world.cellW - 2);
        }
    }
    
    private Color getColor(float pValue)
    {
        if (pValue <= -10000)
        {
            //Terrain. Brown color.
            return new Color(150, 85, 41);//158, 114, 17);
        }
        else if (pValue < 0)
        {
            pValue = pValue  * -1;
            
            int R = (int)Math.round(pValue);
            int B = R / 2;
            int G = R / 2;
            if (R > 255) R = 255;
            if (G > 255) G = 255;
            if (B > 255) B = 255;
            return new Color(R, G, B);
        }
        else if (pValue > 0)
        {
            int B = (int)Math.round(pValue);
            int R = B / 2;
            int G = B / 2;
            if (R > 255) R = 255;
            if (G > 255) G = 255;
            if (B > 255) B = 255;
            return new Color(R, G, B);
        }
        
        return Color.black;
    }
}
