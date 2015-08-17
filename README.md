# Markup Converter
An extensible web app that converts markup.

## Installation
This is a standard ASP.NET MVC web app.  To install, follow [this Microsoft article](http://www.asp.net/web-forms/overview/deployment/web-deployment-in-the-enterprise/manually-installing-web-packages)

## Plugin API
You can add your own markup language!

In a nutshell:

1. Create a public class that implements the IMarkupLanguage interface
1. Compile 
1. Drop the dll into the same directory as the MarkupConverterServiceApi dll 
1. Restart the web app.

## Architecture Overview
This application uses Ninject to achieve dependency injection.  The Web project does not have any direct references to any "impl" projects (only to an Api project and a Modules project).  Similarly, plugins only reference the Api and not the "Impl". 


### Diagram
<pre><code>
      MarkupConverterWeb

  |							\	
  |							 \	
  v							  \	
                               v
 Modules
         \
		   -------
		  	      \--> MarkupConverterServiceApi
   |						
   |                    ^           ^        ^
   |                    |           |         \
   v                    |			|	   ^   \
									|	   |	\
MarkupConverterServiceLocal         |      \
									|		|	FrontlineMarkupLanguagePlugin
									|	  PluginX
									PluginY
</code></pre>