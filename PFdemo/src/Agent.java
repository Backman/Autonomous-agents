import java.awt.Point;
import java.util.Vector;

/**
 *
 * @author Johan Hagelbäck
 */
public class Agent extends WObject 
{
    private final int TAIL_SIZE = 10;
    public Vector<Point> trail;
    public int id;
    
    public Agent(int x, int y)
    {
        this.x = x;
        this.y = y;
        trail = new Vector<Point>();
        updateTail(new Point(x, y));
        id = MapGenerator.agent_ID;
        MapGenerator.agent_ID++;
    }
    
    public int getSize()
    {
        return PF.getInstance().oUnit.length;
    }
    
    public float[][] getPF()
    {
        return PF.getInstance().oUnit;
    }
    
    public void updateTail(Point p)
    {
        if (trail.size() > 0)
        {
            Point l = trail.lastElement();
            if (l.x == p.x && l.y == p.y) return; //Not add if already in list
        }
        
        trail.add(p);
        if (trail.size() >= TAIL_SIZE)
        {
            trail.remove(0);
        }
    }
}
