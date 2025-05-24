# DesiKhataApp Icon Creation Instructions

## App Icon
1. Create a PNG image of size 512x512 pixels with the following design:
   - Green background (#2e7d32)
   - Ledger book styling with white rectangle
   - Horizontal lines mimicking a ledger book
   - Red rupee symbol (₹) in the center

2. Save this as "khata_icon.png" in the Resources/Images folder

## Splash Screen
1. Create a PNG image of size 512x512 pixels with:
   - Green background (#2e7d32)
   - White circle in the center
   - Red rupee symbol (₹) in the center of the circle
   - "DESI KHATA" text at the bottom
   - "Digital Ledger System" tagline below that

2. Save this as "khata_splash.png" in the Resources/Images folder

## Implementation in Code
For displaying the custom icon in various places in the app:

```csharp
// In XAML:
<Image Source="khata_icon.png" WidthRequest="64" HeightRequest="64" />

// In C#:
var image = new Image
{
    Source = "khata_icon.png",
    WidthRequest = 64,
    HeightRequest = 64
};
```

Note: These custom images will be used alongside the default app icon and splash screen that are generated from SVG files. The custom images can be used within the app's UI as needed.
