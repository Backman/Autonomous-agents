/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */


import java.awt.*;
import javax.swing.*;

/**
 *
 * @author admin
 */
public class Swarm extends JApplet {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        new Swarm();
    }
    
    public Swarm()
    {
        startApplet();
        //startApplication();
    }
    
    private void startApplet()
    {
        this.setSize(650, 435);
        this.getContentPane().setLayout(new FlowLayout());
        this.getContentPane().add(new GUI());
        this.setVisible(true);
    }
    
    private void startApplication()
    {
        JFrame frame = new JFrame("Agent Swarm - Boids");
        frame.setSize(650,435);
        frame.setDefaultCloseOperation(WindowConstants.EXIT_ON_CLOSE);
        frame.getContentPane().setLayout(new FlowLayout());
        frame.getContentPane().add(new GUI());
        frame.setVisible(true);
    }
}
