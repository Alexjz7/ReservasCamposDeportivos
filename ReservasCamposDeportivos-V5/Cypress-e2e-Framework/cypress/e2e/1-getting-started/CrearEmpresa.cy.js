// En tu archivo de prueba Cypress (por ejemplo, cypress/integration/tu-prueba.spec.js)

describe('Prueba de clic en el botón "Ir al Dashboard"', () => {
    beforeEach(() => {
      // Visitar la página antes de cada prueba
      cy.visit('https://localhost:7262');
    });
  
    it('debería redirigir al hacer clic en "Ir al Dashboard"', () => {
      cy.get('[data-test="ir-al-dashboard"]').click();
      cy.url().should('include', '/Access/Login');
  
      //Login
      cy.get('[data-test="input-usuario"]').type('AlexjzE');
      cy.get('[data-test="input-pass"]').type('12345678');
      cy.get('[data-test="btn-login"]').click();
      cy.url().should('include', '/Home/DisponibilidadCampos');
  
      //Verificamos el tipo de usuario logueado sea administrador
      cy.get('[data-test="tipoUser"]').invoke('text').then((tipoUsuario) => {
        cy.log('Tipo de usuario:', tipoUsuario);

    if (tipoUsuario === 'Administrador de empresa') {
          cy.contains('Texto o elemento específico para Administrador de Empresa').should('exist');
        }
      });

      //nos dirigimos a crear empresa
    });
  });
  