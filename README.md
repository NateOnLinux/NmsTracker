# NMS Tracker
NMS Tracker is a desktop and web tool for visualizing explored regions in *No Man's Sky*. It provides an interactive map to help you keep track of your explorations and points of interest. You can load regions and solar systems from your own save file or input coordinates to add new points of interest.  

## Prototype Scope
The initial prototype focuses on validating the concept with a simple, interactive 2D map of discovered regions and a minimal supporting backend.  

### Planned prototype capabilities
- [ ] 2D visualization of basic discoveries
  - [ ] Sectors
  - [ ] Systems
- [ ] Highlight Player current / previous coordinates
- [ ] Convert between Galactic Coordinates, Portal Coordinates, and Universal Addresses in UI
- [ ] User-generated POIs (Sector, System, Planet)
- [ ] Persistent POI Notes  

The game’s save data does not include positions for individual star systems within a region. To avoid displaying misleading information, star systems, planets, flora, fauna, and minerals will be presented as lists.

## Planned Features
Beyond the initial prototype, planned functionality includes the following, in no particular order: 
- Black hole tracking
- Additional discovery visualization:
  - Planets
  - Counts of flora, fauna, and minerals
- Player-provided metadata for discoveries (Names, Attributes, Images)
- Search across discoveries by metadata or partial address
- Online discovery sync
- Import and Export of discovery data
- Full-featured web UI
- Enhanced player state tracking
- Improved save file management
- Custom save file parser

## Save File Safety  
NMS Tracker **does not write to your save file.** The application uses [`libNOM.io`](https://github.com/zencq/libNOM.io) to read discovery data and monitor the player's current position.      
For added safety, the application has the option to create snapshots with `libNOM.io`.  
This software is provided without warranty. Use of NMS Tracker is at your own risk.   

## Project Status
This project is in a very early phase. Breaking changes, refactors, and tooling adjustments are expected as requirements and architecture are refined.


## Credits
This project builds on the work and research of the *No Man's Sky* community and explores alternative approaches to discovery tracking.   
- Thanks to [goatfungus](https://github.com/goatfungus) for [NMSSaveEditor](https://github.com/goatfungus/NMSSaveEditor) which I used to understand the save file structure.    
- Inspiration for this project came from [Pilgrim Star Path](https://pahefu.github.io/pilgrimstarpath/) made by [pahefu](https://github.com/pahefu).  
- Coordinate conversion details for galactic, portal, and universal addresses came from the [No Man's Sky Wiki](https://nomanssky.miraheze.org/wiki/Universal_Address).  
- This project makes use of [libNOM.io](https://github.com/zencq/libNOM.io) by [zencq](https://github.com/zencq) to ensure compatibility across game versions. See `/licenses` for license details.
