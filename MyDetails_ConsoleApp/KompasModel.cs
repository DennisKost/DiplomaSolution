using System;
using Kompas6Constants3D;
using Kompas6API5;

namespace MyDetails_ConsoleApp
{
    /// <summary>
    /// Логика рисования деталей в компасе
    /// </summary>
    class KompasModel
    {
        private KompasObject kompas;

        public KompasModel()
        {
            Type t = Type.GetTypeFromProgID("KOMPAS.Application.5");
            kompas = (KompasObject)Activator.CreateInstance(t);
            kompas.ActivateControllerAPI();
        }

        /// <summary>
        /// Нарисовать деталь без отверстий
        /// </summary>
        public void DrawWithoutHoles()
        {
            if (kompas != null)
            {
                kompas.Visible = true;
                ksDocument3D iDocument3D = (ksDocument3D)kompas.Document3D();
                if (iDocument3D.Create(false /*видимый*/, true /*деталь*/))
                {
                    iDocument3D.fileName = "Without Holes";
                    ksPart iPart = (ksPart)iDocument3D.GetPart((short)Part_Type.pTop_Part);	// новый компонент (.pNew_Part - был) 
                    if (iPart != null)
                    {
                        //// получим интерфейс базовой плоскости XOY
                        ksEntity planeXOY = (ksEntity)iPart.GetDefaultEntity((short)Obj3dType.o3d_planeXOY);    // 1-интерфейс на плоскость XOY
                        ksEntity iSketch = (ksEntity)iPart.NewEntity((short)Obj3dType.o3d_sketch);
                        if (iSketch != null)
                        {
                            // интерфейс свойств эскиза
                            ksSketchDefinition iDefinitionSketch = (ksSketchDefinition)iSketch.GetDefinition();
                            if (iDefinitionSketch != null)
                            {
                                iDefinitionSketch.SetPlane(planeXOY);
                                iSketch.Create();
                                ksDocument2D iDocument2D = (ksDocument2D)iDefinitionSketch.BeginEdit();
                                iDocument2D.ksLineSeg(-30.0, -30.0, 30.0, -30.0, 1);
                                iDocument2D.ksLineSeg(30.0, -30.0, 0, 30.0, 1);
                                iDocument2D.ksLineSeg(0, 30.0, -60.0, 30.0, 1);
                                iDocument2D.ksLineSeg(-60.0, 30.0, -30.0, -30.0, 1);
                                iDefinitionSketch.EndEdit();
                                ksEntity entityExtr = (ksEntity)iPart.NewEntity((short)Obj3dType.o3d_bossExtrusion);
                                if (entityExtr != null)
                                {
                                    // интерфейс свойств базовой операции выдавливания
                                    ksBossExtrusionDefinition extrusionDef = (ksBossExtrusionDefinition)entityExtr.GetDefinition(); // интерфейс базовой операции выдавливания
                                    if (extrusionDef != null)
                                    {
                                        ksExtrusionParam extrProp = (ksExtrusionParam)extrusionDef.ExtrusionParam(); // интерфейс структуры параметров выдавливания
                                        if (extrProp != null)
                                        {
                                            extrusionDef.SetSketch(iSketch); // эскиз операции выдавливания
                                            extrProp.direction = (short)Direction_Type.dtNormal;      // направление выдавливания (прямое)
                                            extrProp.typeNormal = (short)End_Type.etBlind;      // тип выдавливания (строго на глубину)
                                            extrProp.depthNormal = 10;         // глубина выдавливания
                                            entityExtr.Create();                // создадим операцию
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Нарисовать деталь с круглым отверстием
        /// </summary>
        public void DrawWithRoundHole()
        {
            if (kompas != null)
            {
                kompas.Visible = true;
                ksDocument3D iDocument3D = (ksDocument3D)kompas.Document3D();
                if (iDocument3D.Create(false /*видимый*/, true /*деталь*/))
                {
                    iDocument3D.fileName = "With Round Hole";
                    ksPart iPart = (ksPart)iDocument3D.GetPart((short)Part_Type.pTop_Part);	// новый компонент (.pNew_Part - был) 
                    if (iPart != null)
                    {
                        //// получим интерфейс базовой плоскости XOY
                        ksEntity planeXOY = (ksEntity)iPart.GetDefaultEntity((short)Obj3dType.o3d_planeXOY);    // 1-интерфейс на плоскость XOY
                        ksEntity iSketch = (ksEntity)iPart.NewEntity((short)Obj3dType.o3d_sketch);
                        if (iSketch != null)
                        {
                            // интерфейс свойств эскиза
                            ksSketchDefinition iDefinitionSketch = (ksSketchDefinition)iSketch.GetDefinition();
                            if (iDefinitionSketch != null)
                            {
                                iDefinitionSketch.SetPlane(planeXOY);
                                iSketch.Create();
                                ksDocument2D iDocument2D = (ksDocument2D)iDefinitionSketch.BeginEdit();
                                iDocument2D.ksLineSeg(-30.0, -30.0, 30.0, -30.0, 1);
                                iDocument2D.ksLineSeg(30.0, -30.0, 0, 30.0, 1);
                                iDocument2D.ksLineSeg(0, 30.0, -60.0, 30.0, 1);
                                iDocument2D.ksLineSeg(-60.0, 30.0, -30.0, -30.0, 1);
                                iDocument2D.ksCircle(-15, 0, 4, 1);
                                iDefinitionSketch.EndEdit();
                                ksEntity entityExtr = (ksEntity)iPart.NewEntity((short)Obj3dType.o3d_bossExtrusion);
                                if (entityExtr != null)
                                {
                                    // интерфейс свойств базовой операции выдавливания
                                    ksBossExtrusionDefinition extrusionDef = (ksBossExtrusionDefinition)entityExtr.GetDefinition(); // интерфейс базовой операции выдавливания
                                    if (extrusionDef != null)
                                    {
                                        ksExtrusionParam extrProp = (ksExtrusionParam)extrusionDef.ExtrusionParam(); // интерфейс структуры параметров выдавливания
                                        if (extrProp != null)
                                        {
                                            extrusionDef.SetSketch(iSketch); // эскиз операции выдавливания
                                            extrProp.direction = (short)Direction_Type.dtNormal;      // направление выдавливания (прямое)
                                            extrProp.typeNormal = (short)End_Type.etBlind;      // тип выдавливания (строго на глубину)
                                            extrProp.depthNormal = 10;         // глубина выдавливания
                                            entityExtr.Create();                // создадим операцию
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Нарисовать деталь с отверстием некруглым
        /// </summary>
        public void DrawWithNonCircularHole()
        {
            if (kompas != null)
            {
                kompas.Visible = true;
                ksDocument3D iDocument3D = (ksDocument3D)kompas.Document3D();
                if (iDocument3D.Create(false /*видимый*/, true /*деталь*/))
                {
                    iDocument3D.fileName = "With Non-Circular Hole";
                    ksPart iPart = (ksPart)iDocument3D.GetPart((short)Part_Type.pTop_Part);	// новый компонент (.pNew_Part - был) 
                    if (iPart != null)
                    {
                        //// получим интерфейс базовой плоскости XOY
                        ksEntity planeXOY = (ksEntity)iPart.GetDefaultEntity((short)Obj3dType.o3d_planeXOY);    // 1-интерфейс на плоскость XOY
                        ksEntity iSketch = (ksEntity)iPart.NewEntity((short)Obj3dType.o3d_sketch);
                        if (iSketch != null)
                        {
                            // интерфейс свойств эскиза
                            ksSketchDefinition iDefinitionSketch = (ksSketchDefinition)iSketch.GetDefinition();
                            if (iDefinitionSketch != null)
                            {
                                iDefinitionSketch.SetPlane(planeXOY);
                                iSketch.Create();
                                ksDocument2D iDocument2D = (ksDocument2D)iDefinitionSketch.BeginEdit();
                                iDocument2D.ksLineSeg(-30.0, -30.0, 30.0, -30.0, 1);
                                iDocument2D.ksLineSeg(30.0, -30.0, 0, 30.0, 1);
                                iDocument2D.ksLineSeg(0, 30.0, -60.0, 30.0, 1);
                                iDocument2D.ksLineSeg(-60.0, 30.0, -30.0, -30.0, 1);
                                iDocument2D.ksLineSeg(-10.0, -18.660254, -28.660254, 13.660254, 1);
                                iDocument2D.ksLineSeg(-20, 18.660254, -1.339746, -13.660254, 1);
                                iDocument2D.ksArcByAngle(-24.330127, 16.160254, 5, 30.0, 210.0, 1, 1);
                                iDocument2D.ksArcByAngle(-5.669873, -16.160254, 5, 210.0, 30.0, 1, 1);
                                iDefinitionSketch.EndEdit();
                                ksEntity entityExtr = (ksEntity)iPart.NewEntity((short)Obj3dType.o3d_bossExtrusion);
                                if (entityExtr != null)
                                {
                                    // интерфейс свойств базовой операции выдавливания
                                    ksBossExtrusionDefinition extrusionDef = (ksBossExtrusionDefinition)entityExtr.GetDefinition(); // интерфейс базовой операции выдавливания
                                    if (extrusionDef != null)
                                    {
                                        ksExtrusionParam extrProp = (ksExtrusionParam)extrusionDef.ExtrusionParam(); // интерфейс структуры параметров выдавливания
                                        if (extrProp != null)
                                        {
                                            extrusionDef.SetSketch(iSketch); // эскиз операции выдавливания
                                            extrProp.direction = (short)Direction_Type.dtNormal;      // направление выдавливания (прямое)
                                            extrProp.typeNormal = (short)End_Type.etBlind;      // тип выдавливания (строго на глубину)
                                            extrProp.depthNormal = 10;         // глубина выдавливания
                                            entityExtr.Create();                // создадим операцию
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Нарисовать деталь с круглым и некруглым отверстиями
        /// </summary>
        public void DrawWithRoundAndNonCircularHoles()
        {
            if (kompas != null)
            {
                kompas.Visible = true;
                ksDocument3D iDocument3D = (ksDocument3D)kompas.Document3D();
                if (iDocument3D.Create(false /*видимый*/, true /*деталь*/))
                {
                    iDocument3D.fileName = "With Round and Non Circular Holes";
                    ksPart iPart = (ksPart)iDocument3D.GetPart((short)Part_Type.pTop_Part);	// новый компонент (.pNew_Part - был) 
                    if (iPart != null)
                    {
                        //// получим интерфейс базовой плоскости XOY
                        ksEntity planeXOY = (ksEntity)iPart.GetDefaultEntity((short)Obj3dType.o3d_planeXOY);    // 1-интерфейс на плоскость XOY
                        ksEntity iSketch = (ksEntity)iPart.NewEntity((short)Obj3dType.o3d_sketch);
                        if (iSketch != null)
                        {
                            // интерфейс свойств эскиза
                            ksSketchDefinition iDefinitionSketch = (ksSketchDefinition)iSketch.GetDefinition();
                            if (iDefinitionSketch != null)
                            {
                                iDefinitionSketch.SetPlane(planeXOY);
                                iSketch.Create();
                                ksDocument2D iDocument2D = (ksDocument2D)iDefinitionSketch.BeginEdit();
                                iDocument2D.ksLineSeg(-30.0, -30.0, 30.0, -30.0, 1);
                                iDocument2D.ksLineSeg(30.0, -30.0, 0, 30.0, 1);
                                iDocument2D.ksLineSeg(0, 30.0, -60.0, 30.0, 1);
                                iDocument2D.ksLineSeg(-60.0, 30.0, -30.0, -30.0, 1);
                                iDocument2D.ksLineSeg(-9.40983, 11.18034, -31.77051, 11.18034, 1);
                                iDocument2D.ksLineSeg(-31.77051, 11.18034, -20.59017, -11.18034, 1);
                                iDocument2D.ksLineSeg(-20.59017, -11.18034, 1.77051, -11.18034, 1);
                                iDocument2D.ksLineSeg(1.77051, -11.18034, -9.40983, 11.18034, 1);
                                iDocument2D.ksCircle(-45.885255, 20.59017, 4, 1);
                                iDocument2D.ksCircle(-25.295085, 20.59017, 4, 1);
                                iDocument2D.ksCircle(-4.704921, 20.59017, 4, 1);
                                iDocument2D.ksCircle(15.885249, -20.59017, 4, 1);
                                iDocument2D.ksCircle(-4.704915, -20.59017, 4, 1);
                                iDocument2D.ksCircle(-25.295085, -20.59017, 4, 1);
                                iDefinitionSketch.EndEdit();
                                ksEntity entityExtr = (ksEntity)iPart.NewEntity((short)Obj3dType.o3d_bossExtrusion);
                                if (entityExtr != null)
                                {
                                    // интерфейс свойств базовой операции выдавливания
                                    ksBossExtrusionDefinition extrusionDef = (ksBossExtrusionDefinition)entityExtr.GetDefinition(); // интерфейс базовой операции выдавливания
                                    if (extrusionDef != null)
                                    {
                                        ksExtrusionParam extrProp = (ksExtrusionParam)extrusionDef.ExtrusionParam(); // интерфейс структуры параметров выдавливания
                                        if (extrProp != null)
                                        {
                                            extrusionDef.SetSketch(iSketch); // эскиз операции выдавливания
                                            extrProp.direction = (short)Direction_Type.dtNormal;      // направление выдавливания (прямое)
                                            extrProp.typeNormal = (short)End_Type.etBlind;      // тип выдавливания (строго на глубину)
                                            extrProp.depthNormal = 10;         // глубина выдавливания
                                            entityExtr.Create();                // создадим операцию
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}