<?xml version="1.0" encoding="utf-8"?>
<configurationSectionModel xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="e831441f-87a4-4f21-9386-7cf10ea59b4b" namespace="EApp.Data.Configuration" xmlSchemaNamespace="urn:EApp.Data.Configuration" assemblyName="EApp.Data.Configuration" xmlns="http://schemas.microsoft.com/dsltools/ConfigurationSectionDesigner">
  <typeDefinitions>
    <externalType name="String" namespace="System" />
    <externalType name="Boolean" namespace="System" />
    <externalType name="Int32" namespace="System" />
    <externalType name="Int64" namespace="System" />
    <externalType name="Single" namespace="System" />
    <externalType name="Double" namespace="System" />
    <externalType name="DateTime" namespace="System" />
    <externalType name="TimeSpan" namespace="System" />
  </typeDefinitions>
  <configurationElements>
    <configurationSection name="EAppDataConfigurationSection" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="EAppData">
      <elementProperties>
        <elementProperty name="EntityMappings" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="entityMappings" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/e831441f-87a4-4f21-9386-7cf10ea59b4b/EntityMappingConfigurationCollection" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElementCollection name="EntityMappingConfigurationCollection" collectionType="AddRemoveClearMap" xmlItemName="add" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/e831441f-87a4-4f21-9386-7cf10ea59b4b/EntityMappingElement" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="EntityMappingElement">
      <attributeProperties>
        <attributeProperty name="Name" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="name" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/e831441f-87a4-4f21-9386-7cf10ea59b4b/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="File" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="file" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/e831441f-87a4-4f21-9386-7cf10ea59b4b/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
  </configurationElements>
  <propertyValidators>
    <validators />
  </propertyValidators>
</configurationSectionModel>