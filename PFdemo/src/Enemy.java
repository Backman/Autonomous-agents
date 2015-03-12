
/**
 *
 * @author Johan Hagelbäck
 */
public class Enemy extends WObject 
{
    public Enemy(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    
    public int getSize()
    {
        return PF.getInstance().eUnit.length;
    }
    
    public float[][] getPF()
    {
        return PF.getInstance().eUnit;
    }
}
