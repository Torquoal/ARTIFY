# ARTIFY Spatial Prototyping Framework

## Overview
The ARTIFY Spatial Prototyping Framework is an AR-based tool for spatially prototyping and visualizing system workflows in real-world spaces. It enables users to create, manipulate, and connect interactive blocks that represent system components, making it particularly valuable for:

- Manufacturing system layout planning
- Process flow visualization
- Spatial workflow optimization
- Interactive system demonstrations
- AR-based system documentation

## Core Components

### Block System
The framework uses a modular block system with several specialized types:

- **Source Blocks**: Starting points that generate initial inputs
- **Processor Blocks**: Transform inputs into outputs
- **Multiprocessor Blocks**: Handle multiple inputs to create outputs
- **Assembler Blocks**: Combine multiple inputs
- **Disassembler Blocks**: Split inputs into multiple outputs
- **Destination Blocks**: Endpoint blocks that receive final outputs

### Key Features

#### Block Customization
- Shape modification (Cube, Sphere, Cylinder, Table)
- Size scaling
- Rotation adjustment
- Custom naming and labeling
- Input/Output specification

#### Spatial Interaction
- AR placement in real-world space
- Intuitive positioning and scaling
- Visual connection lines between related blocks
- Interactive edit menus

#### System State
- Active/Inactive states
- Visual feedback for correct/incorrect connections
- Real-time validation of input/output relationships

## Technical Structure

### Core Scripts
- `SharedLogic.cs`: Central logic for block behavior and interaction
- `SaveSystem.cs`: Handles saving and loading of block configurations
- `SpawnMenu.cs`: Manages block creation and placement

### Block Scripts
- `BaseBlock.cs`: Foundation class for all block types
- Individual block type scripts (Source.cs, Processor.cs, etc.)
- Data structure scripts for serialization

### Prefabs
- Block type prefabs with pre-configured components
- UI prefabs for interaction and editing
- Shape prefabs for visual representation

## Usage

### Basic Workflow
1. Spawn blocks using the spatial menu
2. Position blocks in real-world space
3. Customize block properties (shape, size, labels)
4. Connect blocks to establish relationships
5. Validate system flow through visual feedback
6. Save configurations for later use

### Block Interaction
- Tap blocks to access edit menu
- Use edit canvas for text input
- Scale and rotate using gesture controls
- Connect blocks by specifying inputs/outputs

## Implementation Requirements
- Unity with AR Foundation
- Compatible AR-capable device
- TextMeshPro for UI elements
- Proper tag setup for block identification

## Best Practices
- Plan block placement considering real-world constraints
- Use meaningful names and labels for clarity
- Maintain logical flow direction
- Validate connections as you build
- Save configurations regularly

## Future Development
- Additional block types for specific industries
- Enhanced visualization options
- Multi-user collaboration features
- Advanced system validation rules
- Integration with external data sources

## License
[TBH]