Feature: Should implement all crud operations with categories
    
    Scenario: Should return all avaliable categories
        Given categories
        When get categories using CategoryService
        Then categories should be the same
        
    Scenario: Should create new category
        Given new category
        When create new category
        Then category should be created
        
    Scenario: Should update category
        Given old and new categories
        When update an existing category
        Then category should be updated