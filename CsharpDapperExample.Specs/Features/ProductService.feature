Feature: Should implement all crud operations with products
    
    Scenario: Should return all avaliable products
        Given products
        When get products using ProductService
        Then products should be the same
        
    Scenario: Should create new product
        Given new product
        When create new product
        Then product should be created
        
    Scenario: Should update product
        Given old and new products
        When update an existing product
        Then product should be updated