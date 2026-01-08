# NMS Tracker
NMS Tracker is a desktop and web application for visualizing discovery data from *No Man's Sky*. It focuses on tracking discovered regions and generating spatial visualizations that support long-distance navigation across the galaxy.

The primary goal is to provide a clear representation of galaxies and the relative positions of regions within them. By visualizing these spatial relationships, the tool enables players to plot and execute warp-based travel routes rather than relying on portal travel.

## Problem Statement  
The discovery tracking system and galaxy map in *No Man’s Sky* present distinct challenges when it comes to mapping and long-distance navigation. Effective mapping requires spatial visualization in two or three dimensions, allowing players to browse visited regions relative to one another. Interstellar navigation also benefits from spatial reasoning and multi-dimensional visualization. NMS Tracker addresses both needs by providing tools for spatially-aware discovery tracking and route planning.  

## Prototype Scope
The initial prototype focuses on validating core data models, coordinate transformations, and basic spatial visualization using an interactive 2D scatter plot.

### Planned capabilities:
- [ ] 2D visualization of basic discoveries
  - [ ] Sectors
  - [ ] Systems
- [ ] Highlight Player current / previous coordinates
- [ ] Convert between Galactic Coordinates, Portal Coordinates, and Universal Addresses
- [ ] Set a destination and plan a route

The save file does not include the relative positions of star systems within a region. Rather than plotting star systems in approximate-but-incorrect locations, the visualization will be limited to only plotting regions. Selecting a region will reveal a list of discovered star systems.

## Planned Features
Beyond the initial prototype, the planned functionality includes
- Additional discovery visualization:
  - Planets
  - Counts of flora, fauna, and minerals
- Player-provided metadata for discoveries (Names, Attributes, Images)
- Search across discoveries by metadata or partial address
- Online discovery sync
- Import and Export of discovery data
- Full-featured web UI
- Enhanced player state tracking
- Enhanced save file management
- Custom save file parser

Architecture notes and design decisions are documented under `/docs`/.  
This project is in an early and unstable phase. Breaking changes, refactors, and tooling adjustments are expected as requirements and architecture are refined.

## Credits
This project builds on top of the work of the *No Man's Sky* community.   
- Thanks to [goatfungus](https://github.com/goatfungus) for [NMSSaveEditor](https://github.com/goatfungus/NMSSaveEditor) which I used to understand the save file structure.    
- Inspiration for this project came from [Pilgrim Star Path](https://pahefu.github.io/pilgrimstarpath/) made by [pahefu](https://github.com/pahefu).  
- Coordinate conversion details for galactic, portal, and universal addresses came from the [No Man's Sky Wiki](https://nomanssky.miraheze.org/wiki/Universal_Address).  
- This project makes use of [libNOM.io](https://github.com/zencq/libNOM.io) by [zencq](https://github.com/zencq) to ensure compatibility across game versions. See 'licenses/' for license details.
