using UnityEngine;
using System;
using Blocks;

// Enum to track different block shapes
public enum BlockShape
{
    Cube,
    Sphere,
    Cylinder,
    Table,
    Container,
    Pipes,
    Pallet,
    PalletJack
}

// Base class for all block data
[Serializable]
public class BlockData
{
    public float[] position;
    public float[] scale;
    public string name;
    public string title;
    public BlockShape shape;

    public BlockData(BaseBlock block)
    {
        position = new float[3] { 
            block.transform.position.x, 
            block.transform.position.y, 
            block.transform.position.z 
        };
        scale = new float[3] { 
            block.transform.localScale.x, 
            block.transform.localScale.y, 
            block.transform.localScale.z 
        };
        name = block.name;
        title = block.title;
        shape = DetermineBlockShape(block);
    }

    // Modify the DetermineBlockShape method in BlockData class
    private BlockShape DetermineBlockShape(BaseBlock block)
    {
        if (block.shape == null) return BlockShape.Cube;

        if (block.shape.CompareTag("Table")) return BlockShape.Table;
        if (block.shape.CompareTag("Cube")) return BlockShape.Cube;
        if (block.shape.CompareTag("Sphere")) return BlockShape.Sphere;
        if (block.shape.CompareTag("Cylinder")) return BlockShape.Cylinder;
        if (block.shape.CompareTag("Container")) return BlockShape.Container;
        if (block.shape.CompareTag("Pipes")) return BlockShape.Pipes;
        if (block.shape.CompareTag("Pallet")) return BlockShape.Pallet;
        if (block.shape.CompareTag("PalletJack")) return BlockShape.PalletJack;
        
        return BlockShape.Cube; // Default fallback
    }
}

[Serializable]
public class ProcessorData : BlockData
{
    public string input_required;
    public string output;
    public string inputSourceName;

    public ProcessorData(Processor processor) : base(processor)
    {
        input_required = processor.input_required;
        output = processor.output;
        inputSourceName = processor.inputSource?.name;
    }
}

[Serializable]
public class SourceData : BlockData
{
    public string output;

    public SourceData(Source source) : base(source)
    {
        output = source.output;
    }
}

[Serializable]
public class DestinationData : BlockData
{
    public string input_required;
    public string inputSourceName;

    public DestinationData(Destination destination) : base(destination)
    {
        input_required = destination.input_required;
        inputSourceName = destination.inputSource?.name;
    }
}

[Serializable]
public class AssemblerData : BlockData
{
    public string input_required1;
    public string input_required2;
    public string output;
    public string inputSourceName1;
    public string inputSourceName2;

    public AssemblerData(Assembler assembler) : base(assembler)
    {
        input_required1 = assembler.input_required1;
        input_required2 = assembler.input_required2;
        output = assembler.output;
        inputSourceName1 = assembler.inputSource1?.name;
        inputSourceName2 = assembler.inputSource2?.name;
    }
}

[Serializable]
public class DisassemblerData : BlockData
{
    public string input_required;
    public string output1;
    public string output2;
    public string inputSourceName;
    public string outputBlockName1;
    public string outputBlockName2;

    public DisassemblerData(Disassembler disassembler) : base(disassembler)
    {
        input_required = disassembler.input_required;
        output1 = disassembler.output1;
        output2 = disassembler.output2;
        inputSourceName = disassembler.inputSource?.name;
        outputBlockName1 = disassembler.outputBlock1?.name;
        outputBlockName2 = disassembler.outputBlock2?.name;
    }
}

[Serializable]
public class MultiprocessorData : BlockData
{
    public string input_required1;
    public string input_required2;
    public string output1;
    public string output2;
    public string inputSourceName1;
    public string inputSourceName2;
    public string outputBlockName1;
    public string outputBlockName2;

    public MultiprocessorData(Multiprocessor multiprocessor) : base(multiprocessor)
    {
        input_required1 = multiprocessor.input_required1;
        input_required2 = multiprocessor.input_required2;
        output1 = multiprocessor.output1;
        output2 = multiprocessor.output2;
        inputSourceName1 = multiprocessor.inputSource1?.name;
        inputSourceName2 = multiprocessor.inputSource2?.name;
        outputBlockName1 = multiprocessor.outputBlock1?.name;
        outputBlockName2 = multiprocessor.outputBlock2?.name;
    }
}

[Serializable]
public class PropData : BlockData
{
    public PropData(Prop prop) : base(prop)
    {
        // No additional data needed for props
    }
}