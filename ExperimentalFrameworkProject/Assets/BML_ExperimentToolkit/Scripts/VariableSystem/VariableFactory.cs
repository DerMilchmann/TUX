﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using BML_ExperimentToolkit.Scripts.ExperimentParts;
using BML_ExperimentToolkit.Scripts.VariableSystem.VariableTypes;
using UnityEngine;

namespace BML_ExperimentToolkit.Scripts.VariableSystem {


    [Serializable]
    public class VariableFactory {

        [SerializeField]
        public SupportedDataTypes DataTypesToCreate;

        [SerializeField]
        public VariableType VariableTypeToCreate;


        readonly Dictionary<SupportedDataTypes, Type> supportedIndependentTypes;
        readonly Dictionary<SupportedDataTypes, Type> supportedDependentTypes;
        readonly Dictionary<SupportedDataTypes, Type> supportedParticipantTypes;

        public VariableFactory() {

            supportedIndependentTypes = GetSupportedTypes(typeof(IndependentVariable));
            supportedDependentTypes = GetSupportedTypes(typeof(DependentVariable));
            supportedParticipantTypes = GetSupportedTypes(typeof(ParticipantVariable));

        }

        static Dictionary<SupportedDataTypes, Type> GetSupportedTypes(Type baseType) {
            
            IEnumerable<Type> concreteTypes = Assembly.GetAssembly(baseType).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract &&
                                 myType.IsSubclassOf(baseType));
            
            
            Dictionary<SupportedDataTypes, Type> createdDic = new Dictionary<SupportedDataTypes, Type>();
            
            foreach (Type concreteType in concreteTypes) {
                dynamic tempVariable = Activator.CreateInstance(concreteType);
                if (!createdDic.ContainsKey(tempVariable.DataType)) createdDic.Add(tempVariable.DataType, concreteType);
            }

            return createdDic;
        }

        #region IndependentVariables
        
        public List<IndependentVariableInt> IntIVs = new List<IndependentVariableInt>();
        public List<IndependentVariableFloat> FloatIVs = new List<IndependentVariableFloat>();
        public List<IndependentVariableString> StringIVs = new List<IndependentVariableString>();
        public List<IndependentVariableBool> BoolIVs = new List<IndependentVariableBool>();
        public List<IndependentVariableGameObject> GameObjectIVs = new List<IndependentVariableGameObject>();
        public List<IndependentVariableVector2> Vector2IVs = new List<IndependentVariableVector2>();
        public List<IndependentVariableVector3> Vector3IVs = new List<IndependentVariableVector3>();
        public List<IndependentVariableCustomDataType> CustomDataTypeIVs = new List<IndependentVariableCustomDataType>();

        #endregion

        #region DependentVariables

        public List<DependentVariableInt> IntDVs = new List<DependentVariableInt>();
        public List<DependentVariableFloat> FloatDVs = new List<DependentVariableFloat>();
        public List<DependentVariableString> StringDVs = new List<DependentVariableString>();
        public List<DependentVariableBool> BoolDVs = new List<DependentVariableBool>();
        public List<DependentVariableGameObject> GameObjectDVs = new List<DependentVariableGameObject>();
        public List<DependentVariableVector2> Vector2DVs = new List<DependentVariableVector2>();
        public List<DependentVariableVector3> Vector3DVs = new List<DependentVariableVector3>();
        public List<DependentVariableCustomDataType> CustomDataTypeDVs = new List<DependentVariableCustomDataType>();

        #endregion

        #region ParticipantVariables

        public List<ParticipantVariableInt> IntParticipantVariables = new List<ParticipantVariableInt>();
        public List<ParticipantVariableFloat> FloatParticipantVariables = new List<ParticipantVariableFloat>();
        public List<ParticipantVariableString> StringParticipantVariables = new List<ParticipantVariableString>();
        public List<ParticipantVariableBool> BoolParticipantVariables = new List<ParticipantVariableBool>();
        public List<ParticipantVariableGameObject> GameObjectParticipantVariables = new List<ParticipantVariableGameObject>();
        public List<ParticipantVariableVector2> Vector2ParticipantVariables = new List<ParticipantVariableVector2>();
        public List<ParticipantVariableVector3> Vector3ParticipantVariables = new List<ParticipantVariableVector3>();
        public List<ParticipantVariableCustomData> CustomDataParticipantVariables = new List<ParticipantVariableCustomData>();

        #endregion


        public Variables Variables {
            get {

                List<Variable> allVariables = new List<Variable>();

                //IVs
                allVariables.AddRange(IntIVs);
                allVariables.AddRange(FloatIVs);
                allVariables.AddRange(StringIVs);
                allVariables.AddRange(BoolIVs);
                allVariables.AddRange(GameObjectIVs);
                allVariables.AddRange(Vector2IVs);
                allVariables.AddRange(Vector3IVs);
                allVariables.AddRange(CustomDataTypeIVs);

                //DVs
                allVariables.AddRange(IntDVs);
                allVariables.AddRange(FloatDVs);
                allVariables.AddRange(StringDVs);
                allVariables.AddRange(BoolDVs);
                allVariables.AddRange(GameObjectDVs);
                allVariables.AddRange(Vector2DVs);
                allVariables.AddRange(Vector3DVs);
                allVariables.AddRange(CustomDataTypeDVs);
                

                //PARTICIPANT VARIABLES
                allVariables.AddRange(IntParticipantVariables);    
                allVariables.AddRange(FloatParticipantVariables);
                allVariables.AddRange(StringParticipantVariables);
                allVariables.AddRange(BoolParticipantVariables);
                allVariables.AddRange(GameObjectParticipantVariables);
                allVariables.AddRange(Vector2ParticipantVariables);
                allVariables.AddRange(Vector3ParticipantVariables);
                allVariables.AddRange(CustomDataParticipantVariables);
                
                
                return new Variables(allVariables);
            }
        }

        public void AddNew() {
            switch (VariableTypeToCreate) {
                
                case VariableType.Independent: 
                    
                    if (supportedIndependentTypes.TryGetValue(DataTypesToCreate, out Type ivType)) {
                        IndependentVariable newIv = Activator.CreateInstance(ivType) as IndependentVariable;
                        UpdateSerializedIvsWith(newIv);
                    }
                    
                    break;
                
                case VariableType.Dependent:

                    if (supportedDependentTypes.TryGetValue(DataTypesToCreate, out Type dvType)) {
                        DependentVariable newDv = Activator.CreateInstance(dvType) as DependentVariable;
                        UpdateSerializedDvsWith(newDv);
                    }

                    break;
                case VariableType.ChooseType:
                    break;
                case VariableType.Participant:

                    if (supportedParticipantTypes.TryGetValue(DataTypesToCreate, out Type pvType)) {
                        ParticipantVariable newPv = Activator.CreateInstance(pvType) as ParticipantVariable;
                        UpdateSerializedPvsWith(newPv);
                    }
                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(VariableTypeToCreate), DataTypesToCreate, null);
            }

        }

        void UpdateSerializedIvsWith(IndependentVariable iv) {

            switch (iv.DataType) {
                case SupportedDataTypes.Int:
                    IntIVs.Add(iv as IndependentVariableInt);
                    break;
                case SupportedDataTypes.Float:
                    FloatIVs.Add(iv as IndependentVariableFloat);
                    break;
                case SupportedDataTypes.String:
                    StringIVs.Add(iv as IndependentVariableString);
                    break;
                case SupportedDataTypes.Bool:
                    BoolIVs.Add(iv as IndependentVariableBool);
                    break;
                case SupportedDataTypes.GameObject:
                    GameObjectIVs.Add(iv as IndependentVariableGameObject);
                    break;
                case SupportedDataTypes.Vector2:
                    Vector2IVs.Add(iv as IndependentVariableVector2);
                    break;
                case SupportedDataTypes.Vector3:
                    Vector3IVs.Add(iv as IndependentVariableVector3);
                    break;
                case SupportedDataTypes.CustomDataType_NotYetImplemented:
                    CustomDataTypeIVs.Add(iv as IndependentVariableCustomDataType);
                    break;
                case SupportedDataTypes.ChooseType:
                    throw new InvalidEnumArgumentException("Trying to create new variable, but not types not yet chosen");
                default:
                    throw new NotImplementedException("Support for this BlockData types has not yet been defined." +
                                                                  "You can customize it yourself in the IndependentVariable.cs class");
            }
            
                       
              
        }

        void UpdateSerializedDvsWith(DependentVariable dv) {
            switch (dv.DataType) {
                        case SupportedDataTypes.Int:
                            IntDVs.Add(dv as DependentVariableInt);
                            break;
                        case SupportedDataTypes.Float:
                            FloatDVs.Add(dv as DependentVariableFloat);
                            break;
                        case SupportedDataTypes.String:
                            StringDVs.Add(dv as DependentVariableString);
                            break;
                        case SupportedDataTypes.Bool:
                            BoolDVs.Add(dv as DependentVariableBool);
                            break;
                        case SupportedDataTypes.GameObject:
                            GameObjectDVs.Add(dv as DependentVariableGameObject);
                            break;
                        case SupportedDataTypes.Vector2:
                            Vector2DVs.Add(dv as DependentVariableVector2);
                            Debug.Log(Vector2DVs.Count);
                            break;
                        case SupportedDataTypes.Vector3:
                            Vector3DVs.Add(dv as DependentVariableVector3);
                            break;
                        case SupportedDataTypes.CustomDataType_NotYetImplemented:
                            CustomDataTypeDVs.Add(dv as DependentVariableCustomDataType);
                            break;
                        case SupportedDataTypes.ChooseType:
                            throw new
                                InvalidEnumArgumentException("Trying to create new variable, but not types not yet chosen");
                        default:
                            throw new NotImplementedException("Support for this BlockData types has not yet been defined." +
                                                              "You can customize it yourself in the IndependentVariable.cs class");
                    }
        }

        void UpdateSerializedPvsWith(ParticipantVariable pv) {
            switch (pv.DataType) {
                        case SupportedDataTypes.Int:
                            IntParticipantVariables.Add(pv as ParticipantVariableInt);
                            break;
                        case SupportedDataTypes.Float:
                            FloatParticipantVariables.Add(pv as ParticipantVariableFloat);
                            break;
                        case SupportedDataTypes.String:
                            StringParticipantVariables.Add(pv as ParticipantVariableString);
                            break;
                        case SupportedDataTypes.Bool:
                            BoolParticipantVariables.Add(pv as ParticipantVariableBool);
                            break;
                        case SupportedDataTypes.GameObject:
                            GameObjectParticipantVariables.Add(pv as ParticipantVariableGameObject);
                            break;
                        case SupportedDataTypes.Vector2:
                            Vector2ParticipantVariables.Add(pv as ParticipantVariableVector2);
                            break;
                        case SupportedDataTypes.Vector3:
                            Vector3ParticipantVariables.Add(pv as ParticipantVariableVector3);
                            break;
                        case SupportedDataTypes.CustomDataType_NotYetImplemented:
                            CustomDataParticipantVariables.Add(pv as ParticipantVariableCustomData);
                            break;
                        case SupportedDataTypes.ChooseType:
                            throw new
                                InvalidEnumArgumentException("Trying to create new variable, but type not yet chosen");
                        default:
                            throw new NotImplementedException("Support for this data type has not yet been defined." +
                                                              "You can customize it yourself or submit a request to include it");
                    }
        }

    }
}