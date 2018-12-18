# WPF Doughnut ProgressBar or Status Indicator

This code is an example to anyone that needs it.

I needed to figure out how to replicate the GitHub Pull Request status circle in WPF.

![img](docs/firefox_2018-12-18_12-33-59.png)

I did read this [stackoverflow: WPF Doughnut ProgressBar](https://stackoverflow.com/questions/36752183/wpf-doughnut-progressbar). It was food for thought but I took a different approach.

I realized I could compute a polygon to convey the progress and apply a mask on top.

I started here by writing the code to create a polygon.

![img](docs/2018-12-18_08-36-53.gif)

Then using clipping and overlaying the polygons was able to create this.

![img](docs/WpfApp1_2018-12-18_09-28-35.png)