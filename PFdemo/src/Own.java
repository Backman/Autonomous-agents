
/**
 *
 * @author Johan Hagelbäck
 */
public class Own extends WObject 
{
    public Own(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    
    public int getSize()
    {
        return PF.getInstance().oUnit.length;
    }
    
    public float[][] getPF()
    {
        return PF.getInstance().oUnit;
    }
}
