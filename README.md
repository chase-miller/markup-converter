# Markup Converter
An extensible web app that converts markup.

## Installation
This is a standard ASP.NET MVC web app.  To install, follow [this Microsoft article](http://www.asp.net/web-forms/overview/deployment/web-deployment-in-the-enterprise/manually-installing-web-packages)

## Plugin API
You can add your own markup language!

In a nutshell, create a class that implements the IMarkupLanguage interface, compile, drop the dll into the plugins directory, and restart the web app.

## Architecture Overview


## Notes
Asp.net mvc with razor 
Be sure to use partial views and base html for common stuff. 
Use bootstrap 
Use Ajax and hit an endpoint to convert

Have controller hit a separate IMarkupConverterService interface (contained within an IMarkupService project). 
In a separate project, have a IMarkupConverterService implement the interface.  DI this with nject.  Make sure sure there aren’t any direct references but to the interface project.
For initial render, controller calls IMarkupConverterService.Languages to get a list of strings that are languages; use this to populate a dropdown list in the html
When clicking button, have controller call string IMarkupConverterService.parseMarkup(“markupLanguage”, “markup”)
Also make available string IMarkupConverterService.convertToMarkup(“markupLanguage”, “plainText”) - throw a NotYetImplementedException from concrete implementation.


MarkupConverter iterates over its list of IMarkupLanguages (inject iwth nject) 
It then calls IMarkupLanguage.parseMarkup(“markup”) -or - IMarkupLanguage.convertToMarkup(“plainText”)
Have a FrontlineMarkup class implement this interface - put it in a separate project to prove out plugin mechanism

Test for errors in user-input syntax. 
Use mvc's error handling framework 

Say in controllers constructor that we use ninject to dependency inject the service so that later on we can easily replace it with, for instance, a proxy to a microservice 

Add a README.txt with installation notes, highlights (interface-based development, references, nject, etc), these notes, and how to plug in your own markup language, architecture diagram 