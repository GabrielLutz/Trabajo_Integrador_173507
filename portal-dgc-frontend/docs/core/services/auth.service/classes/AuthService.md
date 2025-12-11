[**portal-dgc-frontend**](../../../../README.md)

***

[portal-dgc-frontend](../../../../README.md) / [core/services/auth.service](../README.md) / AuthService

# Class: AuthService

Defined in: [src/app/core/services/auth.service.ts:15](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/services/auth.service.ts#L15)

## Constructors

### Constructor

> **new AuthService**(): `AuthService`

#### Returns

`AuthService`

## Methods

### getCurrentUser()

> **getCurrentUser**(): [`SimulatedUser`](../interfaces/SimulatedUser.md) \| `null`

Defined in: [src/app/core/services/auth.service.ts:50](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/services/auth.service.ts#L50)

Retrieves the currently stored simulated user or null when no session exists.

#### Returns

[`SimulatedUser`](../interfaces/SimulatedUser.md) \| `null`

***

### isAuthenticated()

> **isAuthenticated**(): `boolean`

Defined in: [src/app/core/services/auth.service.ts:43](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/services/auth.service.ts#L43)

Checks if a simulated session exists in localStorage.

#### Returns

`boolean`

***

### login()

> **login**(`usuario`, `password`): `boolean`

Defined in: [src/app/core/services/auth.service.ts:23](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/services/auth.service.ts#L23)

Ejecuta un inicio de sesión simulado alineado con RF-01 "Autenticación de usuario" para pruebas de UI.
Acepta cualquier combinación usuario/contraseña no vacía y persiste la sesión en localStorage.

#### Parameters

##### usuario

`string`

##### password

`string`

#### Returns

`boolean`

***

### logout()

> **logout**(): `void`

Defined in: [src/app/core/services/auth.service.ts:36](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/services/auth.service.ts#L36)

Limpia la sesión simulada almacenada en localStorage (RF-01).

#### Returns

`void`

***

### register()

> **register**(`payload`): `boolean`

Defined in: [src/app/core/services/auth.service.ts:58](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/services/auth.service.ts#L58)

Guarda un usuario simulado en localStorage para emular el registro sin backend (RF-02).

#### Parameters

##### payload

[`SimulatedUser`](../interfaces/SimulatedUser.md) & `object`

#### Returns

`boolean`
