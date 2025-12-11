[**portal-dgc-frontend**](../../../../../README.md)

***

[portal-dgc-frontend](../../../../../README.md) / [features/auth/login/login.component](../README.md) / LoginComponent

# Class: LoginComponent

Defined in: [src/app/features/auth/login/login.component.ts:14](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/auth/login/login.component.ts#L14)

## Implements

- `OnInit`
- `OnDestroy`

## Constructors

### Constructor

> **new LoginComponent**(`fb`, `auth`, `router`, `route`): `LoginComponent`

Defined in: [src/app/features/auth/login/login.component.ts:22](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/auth/login/login.component.ts#L22)

#### Parameters

##### fb

`FormBuilder`

##### auth

[`AuthService`](../../../../../core/services/auth.service/classes/AuthService.md)

##### router

`Router`

##### route

`ActivatedRoute`

#### Returns

`LoginComponent`

## Properties

### error

> **error**: `string` = `''`

Defined in: [src/app/features/auth/login/login.component.ts:17](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/auth/login/login.component.ts#L17)

***

### form

> **form**: `FormGroup`

Defined in: [src/app/features/auth/login/login.component.ts:15](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/auth/login/login.component.ts#L15)

***

### loading

> **loading**: `boolean` = `false`

Defined in: [src/app/features/auth/login/login.component.ts:16](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/auth/login/login.component.ts#L16)

***

### successMessage

> **successMessage**: `string` = `''`

Defined in: [src/app/features/auth/login/login.component.ts:18](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/auth/login/login.component.ts#L18)

## Accessors

### f

#### Get Signature

> **get** **f**(): `object`

Defined in: [src/app/features/auth/login/login.component.ts:59](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/auth/login/login.component.ts#L59)

Exposición conveniente de los controles del formulario para el template.

##### Returns

`object`

## Methods

### goToRegistro()

> **goToRegistro**(): `void`

Defined in: [src/app/features/auth/login/login.component.ts:97](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/auth/login/login.component.ts#L97)

Navega al formulario de registro simulado.

#### Returns

`void`

***

### ngOnDestroy()

> **ngOnDestroy**(): `void`

Defined in: [src/app/features/auth/login/login.component.ts:89](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/auth/login/login.component.ts#L89)

Cancela suscripciones activas para evitar fugas de memoria.

#### Returns

`void`

#### Implementation of

`OnDestroy.ngOnDestroy`

***

### ngOnInit()

> **ngOnInit**(): `void`

Defined in: [src/app/features/auth/login/login.component.ts:37](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/auth/login/login.component.ts#L37)

Recupera parámetros de la ruta para mostrar mensajes y configurar el returnUrl.

#### Returns

`void`

#### Implementation of

`OnInit.ngOnInit`

***

### submit()

> **submit**(): `void`

Defined in: [src/app/features/auth/login/login.component.ts:66](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/auth/login/login.component.ts#L66)

Ejecuta el login simulado utilizando AuthService y redirige según corresponda (RF-01).

#### Returns

`void`
