<script setup>
defineProps({
    languages: {
        type: Array,
        required: true
    }
})
</script>
<template>
    <select>
        <option v-for="language in languages" :value="language">{{ language }}</option>
    </select>
</template>