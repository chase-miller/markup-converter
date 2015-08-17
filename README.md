# Markup Converter
An extensible web app that converts markup.

## Installation
This is a standard ASP.NET MVC web app.  To install, follow [this Microsoft article](http://www.asp.net/web-forms/overview/deployment/web-deployment-in-the-enterprise/manually-installing-web-packages)

## Plugin API
You can add your own markup language!

In a nutshell, create a class that implements the IMarkupLanguage interface, compile, drop the dll into the plugins directory, and restart the web app.

## Architecture Overview
This application uses Ninject to achieve dependency injection.  The Web project does not have any direct references to any "impl" projects (only to an Api project and a Modules project).  Similarly, plugins only reference the Api and not the "Impl". 

One weakness is that the Modules project currently knows about each of the plugin projects.  Once I figure out how to use Ninject's assembly scanning ability properly, hopefully this can follow a full slot-in architecture.

### Diagram
<pre><code>
      MarkupConverterWeb

  |							\	
  |							 \	
  v							  \	
                               v
 Modules
				     MarkupConverterServiceApi
   |						
   |                    ^                    ^
   |                    |                     \
   v                    |				   ^   \
										   |	\
MarkupConverterServiceLocal         ^      \
									|		|	FrontlineMarkupLanguagePlugin
									|	  PluginX
									PluginY
</code></pre>