# InvestmentTracker

## Introduction

A simple api and website for saving performance data of your investments.

## Getting started

Add config.js file to the app folder. It should set a __config property on the window that looks something like
```
(function (window) {
  window.__config = window.__config || {};
  window.__config.apiUrl = '{url of hosted api}/api';
  
  window.__config.scraper = {
    investment: '{investment name}',
    password: '{encrypted investment password}',
    url: '{investment url}',
    username: 'encrypted investment username'
  };
}(this));
```

Username and password can be encrypted by calling /api/prices/encrypt?data={plain text to encrypt}&passPhrase={private password}.