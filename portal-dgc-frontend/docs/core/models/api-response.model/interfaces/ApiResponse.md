[**portal-dgc-frontend**](../../../../README.md)

***

[portal-dgc-frontend](../../../../README.md) / [core/models/api-response.model](../README.md) / ApiResponse

# Interface: ApiResponse\<T\>

Defined in: [src/app/core/models/api-response.model.ts:5](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/models/api-response.model.ts#L5)

Contrato genérico de respuesta utilizado en todas las llamadas al backend.
Acompaña los RF expuestos en el portal para describir resultados, mensajes de negocio y errores.

## Type Parameters

### T

`T`

## Properties

### data?

> `optional` **data**: `T`

Defined in: [src/app/core/models/api-response.model.ts:11](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/models/api-response.model.ts#L11)

Carga útil específica del endpoint, en caso de éxito parcial o total.

***

### errors

> **errors**: `string`[]

Defined in: [src/app/core/models/api-response.model.ts:13](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/models/api-response.model.ts#L13)

Listado de errores de validación o negocio retornados por el backend.

***

### message

> **message**: `string`

Defined in: [src/app/core/models/api-response.model.ts:9](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/models/api-response.model.ts#L9)

Mensaje principal a mostrar al usuario con el resultado del proceso.

***

### success

> **success**: `boolean`

Defined in: [src/app/core/models/api-response.model.ts:7](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/models/api-response.model.ts#L7)

Indica si la operación solicitada se resolvió correctamente.
