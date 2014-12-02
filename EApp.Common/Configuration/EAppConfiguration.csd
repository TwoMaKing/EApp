<?xml version="1.0" encoding="utf-8"?>
<configurationSectionModel xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5" namespace="EApp.Common.Configuration" xmlSchemaNamespace="urn:EApp.Common.Configuration" assemblyName="EApp.Common.Configuration" xmlns="http://schemas.microsoft.com/dsltools/ConfigurationSectionDesigner">
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
    <configurationSection name="EAppConfigurationSection" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="EApp">
      <elementProperties>
        <elementProperty name="ObjectContainer" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="objectContainer" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/CurrentObjectContainerElement" />
          </type>
        </elementProperty>
        <elementProperty name="Logger" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="logger" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/CurrentLoggerElement" />
          </type>
        </elementProperty>
        <elementProperty name="Application" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="application" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/CurrentApplicationElement" />
          </type>
        </elementProperty>
        <elementProperty name="AppPlugins" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="appPlugins" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/AppPluginElementCollection" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElement name="CurrentObjectContainerElement">
      <attributeProperties>
        <attributeProperty name="Provider" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="provider" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="InitFromConfigFile" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="initFromConfigFile" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/Boolean" />
          </type>
        </attributeProperty>
        <attributeProperty name="SectionName" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="sectionName" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElement name="CurrentLoggerElement">
      <attributeProperties>
        <attributeProperty name="Provider" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="provider" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElement name="CurrentApplicationElement">
      <attributeProperties>
        <attributeProperty name="Provider" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="provider" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElementCollection name="AppPluginElementCollection" xmlItemName="appPlugin" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/AppPluginElement" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="AppPluginElement">
      <attributeProperties>
        <attributeProperty name="Name" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="name" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Type" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="type" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
  </configurationElements>
  <propertyValidators>
    <validators />
  </propertyValidators>
</configurationSectionModel>