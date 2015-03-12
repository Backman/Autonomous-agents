
/**
 *
 * @author Johan Hagelbäck
 */
public class PF 
{
    public float[][] eUnit;
    public float[][] evUnit;
    public float[][] oUnit;
    public float[][] aSquad;
    public float[][] goal;
    
    private static PF instance = null;
    
    public static PF getInstance()
    {
        if (instance == null)
        {
            instance = new PF();
        }
        return instance;
    }
    
    private PF()
    {
        initFields();
    }
    
    private void initFields()
    {
        initEnemyUnitField();
        initEvadeEnemyUnitField();
        initOwnUnitField();
        initSquadAttractionField();
        initGoalField();
    }
    
    private void initEnemyUnitField()
    {
        eUnit = new float[67][67];
        int cX = 67/2;
        int cY = 67/2;
        
        for (int x = 0; x < 67; x++)
        {
            for (int y = 0; y < 67; y++)
            {
                float d = distance(x, y, cX, cY);
                eUnit[x][y] = calcEnemyUnitPF(d);
            }
        }
    }
    
    private float calcEnemyUnitPF(float dist)
    {
        float p;
        
        if (dist < 8)
        {
            p = dist * 30.0f;
        }
        else if (dist >= 8 && dist < 9)
        {
            p = 240.0f;
        }
        else
        {
            dist -= 9.0f;
            p = 240.0f - dist * 10f;
            if (p < 0) p = 0.0f;
        }
        
        return p;
    }
    
    private void initGoalField()
    {
        goal = new float[101][101];
        int cX = 50;
        int cY = 50;
        
        for (int x = 0; x < 101; x++)
        {
            for (int y = 0; y < 101; y++)
            {
                float d = distance(x, y, cX, cY);
                goal[x][y] = calcGoalPF(d);
            }
        }
    }
    
    private float calcGoalPF(float dist)
    {
        float p = 240.0f - dist * 4.9f;
        if (p < 0) p = 0;
        
        return p;
    }
    
    private void initOwnUnitField()
    {
        oUnit = new float[3][3];
        int cX = 1;
        int cY = 1;
        
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                float d = distance(x, y, cX, cY);
                oUnit[x][y] = calcOwnUnitPF(d);
            }
        }
    }
    
    private float calcOwnUnitPF(float dist)
    {
        float p = 0;
        
        if (dist <= 1.0)
        {
            p = -20;
        }
        else if (dist <= 1.5)
        {
            p = -8;
        }
        
        return p;
    }
    
    private void initSquadAttractionField()
    {
        aSquad = new float[11][11];
        int cX = 5;
        int cY = 5;
        
        for (int x = 0; x < 11; x++)
        {
            for (int y = 0; y < 11; y++)
            {
                float d = distance(x, y, cX, cY);
                aSquad[x][y] = calcSquadAttractionPF(d);
            }
        }
    }
    
    private float calcSquadAttractionPF(float dist)
    {
        float p = 0;
        
        p = 40 - 8.0f * dist;
        if (p < 0) p = 0;    
        
        return p;
    }
    
    private void initEvadeEnemyUnitField()
    {
        evUnit = new float[67][67];
        int cX = 67/2;
        int cY = 67/2;
        
        for (int x = 0; x < 67; x++)
        {
            for (int y = 0; y < 67; y++)
            {
                float d = distance(x, y, cX, cY);
                evUnit[x][y] = calcEvadeEnemyUnitPF(d);
            }
        }
    }
    
    private float calcEvadeEnemyUnitPF(float dist)
    {
        float p;
        
        if (dist <= 8.8)
        {
            p = - 10 -(9-dist) * 16;
            if (p > 0) p = 0;
        }
        else
        {
            p = 200.0f - dist * 6.1f;
            if (p < 0) p = 0;
        }
        
        return p;
    }
    
    private float distance(int x1, int y1, int x2, int y2)
    {
        double A = Math.pow((double)x1 - (double)x2, 2);
        double B = Math.pow((double)y1 - (double)y2, 2);
        return (float)Math.sqrt(A+B);
    }
}
