function filterphotos(sel) {
      var valueSelected = sel.value;
      if(valueSelected === '2')
      {
          $(".falsePicColumn").hide();
          $(".truePicColumn").show();
      }
      if(valueSelected ==='3')
      {
          $(".falsePicColumn").show();
          $(".truePicColumn").hide();
      }
      if(valueSelected ==='1')
      {
          $(".falsePicColumn").show();
          $(".truePicColumn").show();
      }
}