function downloadCsvFile(content, fileName) {
    const blob = new Blob([content], {type: 'text/csv'});
    const link = document.createElement('a');
    link.href = URL.createObjectURL(blob);
    link.download = fileName;
    link.click();
}