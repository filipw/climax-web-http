### 1.2.0
* Added robust CORS configuration helpers

### 1.1.0
* Added support for a nested activator in `PerControllerConfigActivator`
* Added `StringCollectionConstraint` and `EnumConstraint` which can be used as custom route constraints.
* Added `CentralizedPrefixProvider` which allows the devloper to set a global `RoutePrefix`
* Added `SimpleArrayModelBinder<T>` which makes it easy to bind delimited arrays from URI
* Deprecated `StringArrayModelBinder` - in favor of the new `SimpleArrayModelBinder<T>`

### 1.0 - 23 March 2015
* First release.