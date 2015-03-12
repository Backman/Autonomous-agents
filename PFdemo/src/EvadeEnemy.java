
/**
 *
 * @author Johan Hagelbäck
 */
public class EvadeEnemy extends WObject 
{
    public EvadeEnemy(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    
    public int getSize()
    {
        return PF.getInstance().evUnit.length;
    }
    
    public float[][] getPF()
    {
        return PF.getInstance().evUnit;
    }
}
