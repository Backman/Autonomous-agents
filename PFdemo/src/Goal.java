
/**
 *
 * @author Johan Hagelbäck
 */
public class Goal extends WObject 
{
    public Goal(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    
    public int getSize()
    {
        return PF.getInstance().goal.length;
    }
    
    public float[][] getPF()
    {
        return PF.getInstance().goal;
    }
}
