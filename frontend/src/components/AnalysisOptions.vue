<script setup>
/*defineProps({
    results: {
        type: Object,
        required: true
    }
})*/
</script>
<template>
    <label for="labelCount">Number of labels</label>
    <input name="labelCount" type="number" step="1" max="10" min="1" value="10">
    <label for="confidence">Minimum confidence</label>
    <input name="confidence" type="number" step="0.001" max="1" min="0.001" value="0.9">
</template>