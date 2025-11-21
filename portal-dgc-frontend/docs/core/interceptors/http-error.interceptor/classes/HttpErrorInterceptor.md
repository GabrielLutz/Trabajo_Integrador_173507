[**portal-dgc-frontend**](../../../../README.md)

***

[portal-dgc-frontend](../../../../README.md) / [core/interceptors/http-error.interceptor](../README.md) / HttpErrorInterceptor

# Class: HttpErrorInterceptor

Defined in: [src/app/core/interceptors/http-error.interceptor.ts:13](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/interceptors/http-error.interceptor.ts#L13)

## Implements

- `HttpInterceptor`

## Constructors

### Constructor

> **new HttpErrorInterceptor**(): `HttpErrorInterceptor`

#### Returns

`HttpErrorInterceptor`

## Methods

### intercept()

> **intercept**(`req`, `next`): `Observable`\<`HttpEvent`\<`any`\>\>

Defined in: [src/app/core/interceptors/http-error.interceptor.ts:17](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/interceptors/http-error.interceptor.ts#L17)

Centraliza el manejo de errores HTTP y complementa las validaciones de RF-20 para trazabilidad.

#### Parameters

##### req

`HttpRequest`\<`any`\>

##### next

`HttpHandler`

#### Returns

`Observable`\<`HttpEvent`\<`any`\>\>

#### Implementation of

`HttpInterceptor.intercept`
