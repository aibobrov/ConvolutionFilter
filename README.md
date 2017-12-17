# Convolution Filter

In image processing, convolution matrix, or mask is a small matrix. It is used for blurring, sharpening, embossing, edge detection, and more.  

##### Original image
![original](examples/flowers.jpg)

##### Identity filter
Kernel: 

| | | |
|-|-|-|
| 0 | 0 | 0 |
| 0 | 1 | 0 |
| 0 | 0 | 0 |

![identity](examples/flowers_identity.jpg)

##### Edge detection filter
Kernel: 

| | | |
|-|-|-|
| 0 | 1 | 0 |
| 1 | -4 | 1 |
| 0 | 1 | 0 |

![edge-detection](examples/flowers_edged.jpg)


##### Sharpen filter
Kernel: 

| | | | | |
|-|-|-|-|-| 
| 0 | 0 | 0 | 0 | 0 |
| 0 | 0 | 5 | 0 | 0 |
| 0 | -1 | 5 | -1 | 0 |
| 0 | 0 | 1 | 0 | 0 |
| 0 | 0 | 0 | 0 | 0 |

![sharpened](examples/flowers_sharpened.jpg)

##### Box blur filter
Normalization rate = 16

Kernel: 

| | | |
|-|-|-|
| 1 | 2 | 1 |
| 2 | 4 | 2 |
| 1 | 2 | 1 |

![box-blur](examples/flowers_box-blured.jpg)

##### Gaussian blur filter
Normalization rate = 256

Kernel: 

| | | | | | 
|-|-|-|-|-|
| 1 | 4 | 6 | 4 | 1 |
| 4 | 16 | 24 | 16 | 4 |
| 6 | 24 | 36 | 24 | 6 |
| 4 | 16 | 24 | 16 | 4 |
| 1 | 4 | 6 | 4 | 1 |

![gaussian-blur](examples/flowers_gaussian-blured.jpg)

##### Motion blur filter
Normalization rate = 9

Kernel: 

| | | | | | | | | |
|-|-|-|-|-|-|-|-|-|
| 1 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 |
| 0 | 1 | 0 | 0 | 0 | 0 | 0 | 0 | 0 |
| 0 | 0 | 1 | 0 | 0 | 0 | 0 | 0 | 0 |
| 0 | 0 | 0 | 1 | 0 | 0 | 0 | 0 | 0 |
| 0 | 0 | 0 | 0 | 1 | 0 | 0 | 0 | 0 |
| 0 | 0 | 0 | 0 | 0 | 1 | 0 | 0 | 0 |
| 0 | 0 | 0 | 0 | 0 | 0 | 1 | 0 | 0 |
| 0 | 0 | 0 | 0 | 0 | 0 | 0 | 1 | 0 |
| 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 | 1 |


![motion-blur](examples/flowers_motion-blured.jpg)

##### Emboss(Relief) filter
Kernel: 

| | | |
|-|-|-|
| -2 | -1 | 0 |
| -1 | 1 | 1 |
| 0 | 1 | 2 |

![embossed](examples/flowers_embossed.jpg)

For more information visit [this](https://docs.gimp.org/2.6/en/plug-in-convmatrix.html)