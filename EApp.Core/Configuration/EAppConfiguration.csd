<?xml version="1.0" encoding="utf-8"?>
<configurationSectionModel xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5" namespace="EApp.Core.Configuration" xmlSchemaNamespace="urn:EApp.Core.Configuration" assemblyName="EApp.Core.Configuration" xmlns="http://schemas.microsoft.com/dsltools/ConfigurationSectionDesigner">
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
        <elementProperty name="PluginContainer" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="pluginContainer" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/PluginContainerElement" />
          </type>
        </elementProperty>
        <elementProperty name="MiscSettings" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="miscSettings" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/MiscSettingElementCollection" />
          </type>
        </elementProperty>
        <elementProperty name="ResourceManagers" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="resources" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/ResourceElementCollection" />
          </type>
        </elementProperty>
        <elementProperty name="WindowsMvc" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="windowsMvc" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/WindowsMvcElement" />
          </type>
        </elementProperty>
        <elementProperty name="SerializationFormats" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="serializationFormats" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/SerializationFormatElementCollection" />
          </type>
        </elementProperty>
        <elementProperty name="CacheManagers" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="cacheManagers" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/CacheManagerElementCollection" />
          </type>
        </elementProperty>
        <elementProperty name="Redis" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="redis" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/RedisElement" />
          </type>
        </elementProperty>
        <elementProperty name="Handlers" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="handlers" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/HandlerElementCollection" />
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
    <configurationElementCollection name="PluginRegisterElementCollection" xmlItemName="register" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/PluginRegisterElement" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="PluginRegisterElement">
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
    <configurationElement name="PluginContainerElement">
      <elementProperties>
        <elementProperty name="Host" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="host" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/PluginHostElement" />
          </type>
        </elementProperty>
        <elementProperty name="PluginRegisters" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="pluginRegisters" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/PluginRegisterElementCollection" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationElement>
    <configurationElement name="SingleProviderElement">
      <attributeProperties>
        <attributeProperty name="Provider" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="provider" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElement name="PluginHostElement">
      <attributeProperties>
        <attributeProperty name="Provider" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="provider" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
      <elementProperties>
        <elementProperty name="PluginProvider" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="pluginProvider" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/SingleProviderElement" />
          </type>
        </elementProperty>
        <elementProperty name="ServiceProvider" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="serviceProvider" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/SingleProviderElement" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationElement>
    <configurationElementCollection name="ConstructorElementCollection" xmlItemName="paramElement" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/ParamElement" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="ParamElement">
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
      <elementProperties>
        <elementProperty name="Value" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="value" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/SingleValueElement" />
          </type>
        </elementProperty>
        <elementProperty name="Dependency" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="dependency" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/DependencyElement" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationElement>
    <configurationElement name="SingleValueElement">
      <attributeProperties>
        <attributeProperty name="Value" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="value" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElement name="DependencyElement">
      <attributeProperties>
        <attributeProperty name="Type" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="type" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
      <elementProperties>
        <elementProperty name="Constructors" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="constructors" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/ConstructorElementCollection" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationElement>
    <configurationElement name="ProviderElement">
      <attributeProperties>
        <attributeProperty name="Provider" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="provider" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
      <elementProperties>
        <elementProperty name="Constructors" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="constructors" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/ConstructorElementCollection" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationElement>
    <configurationElementCollection name="MiscSettingElementCollection" xmlItemName="add" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/MiscSettingAddElement" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="MiscSettingAddElement">
      <attributeProperties>
        <attributeProperty name="key" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="key" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="value" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="value" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElementCollection name="ResourceElementCollection" xmlItemName="resource" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/NameTypeElement" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="NameTypeElement">
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
    <configurationElementCollection name="ControllerElementCollection" xmlItemName="controller" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/NameTypeElement" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="WindowsMvcElement">
      <elementProperties>
        <elementProperty name="Controllers" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="controllers" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/ControllerElementCollection" />
          </type>
        </elementProperty>
        <elementProperty name="DefaultControllerFactory" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="defaultControllerFactory" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/SingleProviderElement" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationElement>
    <configurationElementCollection name="SerializationFormatElementCollection" xmlItemName="format" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <attributeProperties>
        <attributeProperty name="Default" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="default" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
      <itemType>
        <configurationElementMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/NameTypeElement" />
      </itemType>
    </configurationElementCollection>
    <configurationElementCollection name="CacheManagerElementCollection" xmlItemName="cacheManager" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <attributeProperties>
        <attributeProperty name="Default" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="default" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
      <itemType>
        <configurationElementMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/NameTypeElement" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="RedisElement">
      <attributeProperties>
        <attributeProperty name="WriteHosts" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="writeHosts" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="ReadOnlyHosts" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="readOnlyHosts" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="MaxWritePoolSize" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="maxWritePoolSize" isReadOnly="false" defaultValue="60">
          <type>
            <externalTypeMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/Int32" />
          </type>
        </attributeProperty>
        <attributeProperty name="MaxReadPoolSize" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="maxReadPoolSize" isReadOnly="false" defaultValue="60">
          <type>
            <externalTypeMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/Int32" />
          </type>
        </attributeProperty>
        <attributeProperty name="AutoStart" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="autoStart" isReadOnly="false" defaultValue="&quot;True&quot;">
          <type>
            <externalTypeMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/Boolean" />
          </type>
        </attributeProperty>
        <attributeProperty name="TimeOutSeconds" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="timeOutSeconds" isReadOnly="false" defaultValue="3600">
          <type>
            <externalTypeMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/Int32" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElementCollection name="HandlerElementCollection" xmlItemName="handlerElement" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/6ef3f9ad-8dfe-4172-a7c4-a6c8ea1c07b5/HandlerElement" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="HandlerElement">
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
        <attributeProperty name="Kind" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="kind" isReadOnly="false">
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