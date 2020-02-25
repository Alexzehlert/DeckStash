

export async function simpleFetch(url) {
    const response = await fetch(url)
    return await response.json();
}

export async function simplePOST(url, body) {
    const response = await fetch(url, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(body)
      })
    return await response.json();
}