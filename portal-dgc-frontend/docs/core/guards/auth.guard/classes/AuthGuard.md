[**portal-dgc-frontend**](../../../../README.md)

***

[portal-dgc-frontend](../../../../README.md) / [core/guards/auth.guard](../README.md) / AuthGuard

# Class: AuthGuard

Defined in: [src/app/core/guards/auth.guard.ts:15](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/guards/auth.guard.ts#L15)

## Implements

- `CanActivate`
- `CanLoad`

## Constructors

### Constructor

> **new AuthGuard**(`auth`, `router`): `AuthGuard`

Defined in: [src/app/core/guards/auth.guard.ts:16](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/guards/auth.guard.ts#L16)

#### Parameters

##### auth

[`AuthService`](../../../services/auth.service/classes/AuthService.md)

##### router

`Router`

#### Returns

`AuthGuard`

## Methods

### canActivate()

> **canActivate**(`route`, `state`): `boolean` \| `Observable`\<`boolean`\> \| `Promise`\<`boolean`\>

Defined in: [src/app/core/guards/auth.guard.ts:33](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/guards/auth.guard.ts#L33)

Evita el acceso a rutas activadas cuando el usuario no tiene sesión válida (RF-01).

#### Parameters

##### route

`ActivatedRouteSnapshot`

##### state

`RouterStateSnapshot`

#### Returns

`boolean` \| `Observable`\<`boolean`\> \| `Promise`\<`boolean`\>

#### Implementation of

`CanActivate.canActivate`

***

### canLoad()

> **canLoad**(`route`, `segments`): `boolean` \| `Observable`\<`boolean`\> \| `Promise`\<`boolean`\>

Defined in: [src/app/core/guards/auth.guard.ts:40](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/guards/auth.guard.ts#L40)

Bloquea la carga perezosa de módulos protegidos si el usuario no está autenticado (RF-01).

#### Parameters

##### route

`Route`

##### segments

`UrlSegment`[]

#### Returns

`boolean` \| `Observable`\<`boolean`\> \| `Promise`\<`boolean`\>

#### Implementation of

`CanLoad.canLoad`
