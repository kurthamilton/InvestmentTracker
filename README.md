# InvestmentTracker

## Introduction

A simple api and website for saving performance data of your investments.

## Getting started

Add config.js file to the app folder. It should set a __config property on the window that looks something like
```
(function (window) {
  window.__config = window.__config || {};
  window.__config.apiUrl = '{url of hosted api}/api';
}(this));
```
