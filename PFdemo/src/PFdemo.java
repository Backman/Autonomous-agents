
import java.awt.*;
import javax.swing.*;

/**
 *
 * @author Johan Hagelbäck
 */
public class PFdemo extends JApplet
{

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) 
    {
        new PFdemo();
    }
    
    public PFdemo()
    {
        startApplet();
        //startApplication();
    }
    
    private void startApplet()
    {
        this.setSize(960, 620);
        this.getContentPane().setLayout(new FlowLayout());
        this.getContentPane().add(new GUI());
        this.setVisible(true);
    }
    
    private void startApplication()
    {
        JFrame frame = new JFrame("PF Demo");
        frame.setSize(960,640);
        frame.setDefaultCloseOperation(WindowConstants.EXIT_ON_CLOSE);
        frame.getContentPane().setLayout(new FlowLayout());
        frame.getContentPane().add(new GUI());
        frame.setVisible(true);
    }
}

