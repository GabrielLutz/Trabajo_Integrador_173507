[**portal-dgc-frontend**](../../../../README.md)

***

[portal-dgc-frontend](../../../../README.md) / [core/services/api.service](../README.md) / ApiService

# Class: ApiService

Defined in: [src/app/core/services/api.service.ts:9](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/services/api.service.ts#L9)

## Constructors

### Constructor

> **new ApiService**(`http`): `ApiService`

Defined in: [src/app/core/services/api.service.ts:12](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/services/api.service.ts#L12)

#### Parameters

##### http

`HttpClient`

#### Returns

`ApiService`

## Methods

### delete()

> **delete**\<`T`\>(`endpoint`): `Observable`\<`T`\>

Defined in: [src/app/core/services/api.service.ts:52](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/services/api.service.ts#L52)

#### Type Parameters

##### T

`T`

#### Parameters

##### endpoint

`string`

Ruta relativa del recurso a eliminar.

#### Returns

`Observable`\<`T`\>

Observable con la respuesta del servidor tras eliminar el recurso.

#### Description

Ejecuta una petición HTTP DELETE contra la API del backend.

#### Throws

Cuando el borrado falla en el backend.

***

### get()

> **get**\<`T`\>(`endpoint`): `Observable`\<`T`\>

Defined in: [src/app/core/services/api.service.ts:20](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/services/api.service.ts#L20)

#### Type Parameters

##### T

`T`

#### Parameters

##### endpoint

`string`

Ruta relativa del recurso (sin la URL base).

#### Returns

`Observable`\<`T`\>

Observable con el cuerpo de la respuesta tipado.

#### Description

Ejecuta una petición HTTP GET contra la API del backend.

#### Throws

Cuando la petición falla por problemas de red o estados HTTP distintos de 2xx.

***

### post()

> **post**\<`T`\>(`endpoint`, `data`): `Observable`\<`T`\>

Defined in: [src/app/core/services/api.service.ts:31](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/services/api.service.ts#L31)

#### Type Parameters

##### T

`T`

#### Parameters

##### endpoint

`string`

Ruta relativa del recurso.

##### data

`unknown`

Payload a enviar en el cuerpo de la petición.

#### Returns

`Observable`\<`T`\>

Observable con la respuesta devuelta por el backend.

#### Description

Ejecuta una petición HTTP POST contra la API del backend.

#### Throws

Cuando la operación POST falla.

***

### put()

> **put**\<`T`\>(`endpoint`, `data`): `Observable`\<`T`\>

Defined in: [src/app/core/services/api.service.ts:42](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/services/api.service.ts#L42)

#### Type Parameters

##### T

`T`

#### Parameters

##### endpoint

`string`

Ruta relativa del recurso.

##### data

`unknown`

Datos a persistir en el recurso.

#### Returns

`Observable`\<`T`\>

Observable con la respuesta tipada.

#### Description

Ejecuta una petición HTTP PUT contra la API del backend.

#### Throws

Cuando la operación PUT devuelve un error.
