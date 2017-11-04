# InvestmentTracker

## Introduction

A simple api and website for saving performance data of your investments.

## Getting started

*Hosting locally*
Build the api in Visual Studio and publish. 
Set up an iis site and point at the publish folder

Add config.js file to the app folder. It should set a __config property on the window that looks something like
```
(function (window) {
  window.__config = window.__config || {};
  window.__config.apiUrl = '{url of hosted api}/api';
}(this));
```
