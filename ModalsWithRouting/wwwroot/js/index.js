
 
 if(window.location.pathname !== '/')
 {
     let iframe = document.createElement('iframe');
     iframe.src = window.location;
     iframe.className = 'modal-iframe noselect';
     window.location.href = '/';
     document.body.appendChild(iframe);
 }