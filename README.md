# VSoundPathDemo
Visualisation of sound propagation in vertical seawater coloumn, considering TS-profile

## Basic theory
Since the speed of sound is a function of temperature, pressure and salinity (a function of the density of water, in other words) and the water in the seas and oceans is stratified into layers with different densities, the actual path travelled, for example, by the signal of an acoustic depth gauge (sonar) should be calculated as an integral of speed by time.

## With this application you can:
* load real [TS-profiles](/Profiles) (taken from [NOAA](https://www.nodc.noaa.gov/OC5/SELECT/dbsearch/dbsearch.html))
* See in real-time how sound propagates to the ocean bottom and backwards
* Compare how actual path travelled by sound differs from paths, calculated with a constant speed of sound
* Save animation frames from the simulation

## Misc.
* Calculation of depth, speed of sound in water are based on [UCNLPhysics library](https://github.com/ucnl/UCNLPhysics)  
* To build the app you will also need [UCNLDrivers library](https://github.com/ucnl/UCNLDrivers)
* We appreciate stars!

## Application window
![Screenshot](/Pics/screen1.png)

