
function GetCharactersAjax()
{
    console.log("Start of Ajax method")
    src = "https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js" > 

    $(document).ready(function ()
    {
        for (var i = 1; i <= 9; i++)
        {
            $.ajax({
                url: "/Character/CharacterListJson",
                type: "GET",
                success: function (result) {

                    console.log("Result: " + result.results);

                    $.each(result.results, function (index, item) {
                        $('#CharacterTable').append('<tr>', 
                            '<td> ' + item.name +       '</td >',
                            '<td> ' + item.height +     '</td >',
                            '<td> ' + item.mass +       '</td >',
                            '<td> ' + item.hair_color + '</td >',
                            '<td> ' + item.skin_color + '</td >',
                            '<td> ' + item.eye_color +  '</td >',
                            '<td> ' + item.birth_year + '</td >',
                            '<td> ' + item.gender +     '</td >',
                            '<td> ' + item.homeworld +  '</td >',
                            '</tr >')
                    })
                }
            });
        }
    });
}   